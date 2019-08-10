using Docket.Model;
using Docket.UI.Data;
using Docket.UI.Data.Repository;
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
        private IClientRepository _clientRepository;
        private IEventAggregator _eventAggregator;

        public ClientDetailViewModel(IClientRepository clientDataService,
            IEventAggregator eventAggregator)
        {
            _clientRepository = clientDataService;
            _eventAggregator = eventAggregator;

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
        private bool _hasChanges;

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            { 
                if(_hasChanges != value)
                {
                     _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
   
            }
        }

        public ICommand SaveCommand { get; set; }

        private bool OnSaveCanExecute()
        {
            //TODO: Check if Client is valid
            return Client != null && !Client.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _clientRepository.SaveAsync();
            HasChanges = _clientRepository.HasChanges();
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Publish(
                new AfterClientSavedEventArgs
                {
                    Id = Client.Id,
                    DisplayMember = $"{Client.FirstName} {Client.LastName}"
                });
        }


        public async Task LoadAsync(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            Client = new ClientWrapper(client);
            Client.PropertyChanged += (s , e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _clientRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Client.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }  ;
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        }
    }
}
