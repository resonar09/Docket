using Docket.Model;
using Docket.UI.Data;
using Docket.UI.Event;
using Docket.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Docket.UI.ViewModel
{
    public class ClientDetailViewModel : ViewModelBase, IClientDetailViewModel
    {
        private IClientDataService _clientDataService;
        private IEventAggregator _eventAggregator;

        public ClientDetailViewModel(IClientDataService clientDataService,
            IEventAggregator eventAggregator)
        {
            _clientDataService = clientDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenClientDetailViewEvent>()
                .Subscribe(OnOpenClientDetailView);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private ClientWrapper _client;

        public ClientWrapper Client
        {
            get { return _client; }
            private set
            {
                _client = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; set; }

        private bool OnSaveCanExecute()
        {
            //throw new NotImplementedException();
            //TODO: Chech if Client is valid
            return true;
        }

        private async void OnSaveExecute()
        {
            await _clientDataService.SaveAsync(Client.Model);
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Publish(
                new AfterClientSavedEventArgs
                {
                    Id = Client.Id,
                    DisplayMember = $"{Client.FirstName} {Client.LastName}"
                });
        }

        private async void OnOpenClientDetailView(int clientId)
        {
            await LoadAsync(clientId);
        }

        public async Task LoadAsync(int clientId)
        {
            var client = await _clientDataService.GeByIdAsync(clientId);
            Client = new ClientWrapper(client);
        }


    }
}
