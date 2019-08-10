using Docket.UI.Data.Lookup;
using Docket.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Docket.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IClientLookupDataService _clientLookupDataService;
        private IEventAggregator _eventAggregator;
        private CollectionViewSource clientsCollection;
        private string filterText;

        public NavigationViewModel(IClientLookupDataService clientLookupDataService,
            IEventAggregator eventAggregator)
        {
            _clientLookupDataService = clientLookupDataService;
            _eventAggregator = eventAggregator;
            clientsCollection = new CollectionViewSource();
            Clients = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Subscribe(AfterClientSaved);
        }

        private void AfterClientSaved(AfterClientSavedEventArgs obj)
        {
            var lookupItem = Clients.Single(i => i.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _clientLookupDataService.GetClientLookupAsync();
            Clients.Clear();
            foreach (var item in lookup)
            {
                Clients.Add(new NavigationItemViewModel(item.Id,item.DisplayMember,_eventAggregator));
            }
            clientsCollection.Source = Clients;
            clientsCollection.Filter += usersCollection_Filter;

        }
        public ObservableCollection<NavigationItemViewModel> Clients { get; }
        private NavigationItemViewModel _selectedClient;

        public NavigationItemViewModel SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
                if(_selectedClient != null)
                {
                    _eventAggregator.GetEvent<OpenClientDetailViewEvent>()
                        .Publish(_selectedClient.Id);
                }
            }        
        }
        public string FilterText
        {
            get
            {
                return filterText;
            }
            set
            {
                filterText = value;
                this.clientsCollection.View.Refresh();
                OnPropertyChanged();
            }
        }
        public ICollectionView SourceCollection
        {
            get
            {
                clientsCollection.Source = Clients;
                return this.clientsCollection.View;
            }
        }
        private void usersCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            NavigationItemViewModel client = e.Item as NavigationItemViewModel;
            if (client.DisplayMember.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }


    }
}
