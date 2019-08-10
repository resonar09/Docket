using Docket.UI.Event;
using Docket.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace Docket.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {   
        private string filterText;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        public INavigationViewModel NavigationViewModel { get; }
        private IClientDetailViewModel _clientDetailViewModel;
        public Func<IClientDetailViewModel> _clientDetailViewModelCreator;

        public MainViewModel(INavigationViewModel navigationViewModel, 
            Func<IClientDetailViewModel> clientDetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {

            _clientDetailViewModelCreator = clientDetailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _eventAggregator.GetEvent<OpenClientDetailViewEvent>()
                .Subscribe(OnOpenClientDetailView);
            NavigationViewModel = navigationViewModel;
        }
        public IClientDetailViewModel ClientDetailViewModel
        {
            get { return _clientDetailViewModel; }
            private set
            {
                _clientDetailViewModel = value;
                OnPropertyChanged();
            }
        }
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
        private async void OnOpenClientDetailView(int clientId)
        {
            if(ClientDetailViewModel != null && ClientDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Do you want to cancel you're changes?", "Question");
            if (result == MessageDialogResult.Cancel)
                    return;
            }
            ClientDetailViewModel = _clientDetailViewModelCreator();
            await ClientDetailViewModel.LoadAsync(clientId);
        }


    }
}
