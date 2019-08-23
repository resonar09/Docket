using Docket.Model;
using Docket.UI.Data.Lookups;
using Docket.UI.Data.Repository;
using Docket.UI.Event;
using Docket.UI.View.Services;
using Docket.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using static Docket.Model.LookupItem;

namespace Docket.UI.ViewModel
{
    public class ClientDetailViewModel : ViewModelBase, IClientDetailViewModel
    {
        private IClientRepository _clientRepository;
        private IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;

        public ClientDetailViewModel(IClientRepository clientDataService,
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService,
            IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
        {
            _clientRepository = clientDataService;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            ProgrammingLanguages = new ObservableCollection<LookupItem>();
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

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

        private bool _hasChanges;

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public async Task LoadAsync(int? clientId)
        {
            var client = clientId.HasValue ?
                await _clientRepository.GetByIdAsync(clientId.Value)
                : CreateNewClient();

            InitializeClient(client);

            await LoadProgrammingLanguageLookupAsync();
        }

        private void InitializeClient(Client client)
        {
            Client = new ClientWrapper(client);
            Client.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _clientRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Client.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Client.Id == 0)
            {
                //Trick to trigger validation
                Client.FirstName = "";
                Client.MiddleInitial = "";
                Client.LastName = "";
                Client.City = "";
                Client.Email = "";
                Client.State = "";
                Client.Street = "";
                Client.Zip = "";
            }
        }

        private async Task LoadProgrammingLanguageLookupAsync()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLookupItem { DisplayMember = " - " });
            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
        }

        private Client CreateNewClient()
        {
            var client = new Client();
            _clientRepository.Add(client);
            return client;
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
        private bool OnSaveCanExecute()
        {
            return Client != null && !Client.HasErrors && HasChanges;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the client {Client.FirstName} {Client.LastName}", "Question");
            if (result == MessageDialogResult.OK)
            {
                _clientRepository.Remove(Client.Model);
                await _clientRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterClientDeletedEvent>().Publish(Client.Id);
            }
        }
    }
}
