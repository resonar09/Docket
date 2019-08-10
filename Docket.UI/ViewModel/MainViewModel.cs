using Docket.UI.Event;
using Docket.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Docket.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {   
        private string filterText;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
   
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
            _eventAggregator.GetEvent<AfterClientDeletedEvent>()
                    .Subscribe(AfterClientDeleted);
            CreateNewClientCommand = new DelegateCommand(OnCreateNewClientExecute);
            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }
        public ICommand CreateNewClientCommand { get; }
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
        private async void OnOpenClientDetailView(int? clientId)
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
        private void OnCreateNewClientExecute()
        {
            OnOpenClientDetailView(null);
        }
        private void AfterClientDeleted(int clientId)
        {
            ClientDetailViewModel = null;
        }

    }
}
