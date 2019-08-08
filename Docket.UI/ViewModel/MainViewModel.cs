using Docket.Model;
using Docket.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Docket.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //public ObservableCollection<Client> Clients { get; set; }
        
        private string filterText;
        //private readonly IClientDataService _docketDataService;
        //private Client _selectedClient;
        //private CollectionViewSource clientsCollection;
        public INavigationViewModel NavigationViewModel { get; }
        public IClientDetailViewModel ClientDetailViewModel { get; private set; }

        public MainViewModel(INavigationViewModel navigationViewModel, IClientDetailViewModel clientDetailViewModel)//IDocketDataService docketDataService)
        {
            NavigationViewModel = navigationViewModel;
            ClientDetailViewModel = clientDetailViewModel;
            //clientsCollection = new CollectionViewSource();
            //Clients = new ObservableCollection<Client>();
            //_docketDataService = docketDataService;
        }

        public async Task LoadAsync()
        {
            //var clients = await _docketDataService.GetAllAsync();
            //Clients.Clear();
            //foreach (var client in clients)
            //{
            //    Clients.Add(client);
            //}
            await NavigationViewModel.LoadAsync();
            //clientsCollection.Source = NavigationViewModel.Clients;
            //clientsCollection.Filter += usersCollection_Filter;
        }

        //TODO make these work
        //public Client SelectedClient
        //{
        //    get { return _selectedClient; }
        //    set
        //    {
        //        _selectedClient = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string FilterText
        //{
        //    get
        //    {
        //        return filterText;
        //    }
        //    set
        //    {
        //        filterText = value;
        //        this.clientsCollection.View.Refresh();
        //        OnPropertyChanged();
        //    }
        //}
        //public ICollectionView SourceCollection
        //{
        //    get
        //    {
        //        clientsCollection.Source = NavigationViewModel.Clients;
        //        return this.clientsCollection.View;
        //    }
        //}


     
        //private void usersCollection_Filter(object sender, FilterEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(FilterText))
        //    {
        //        e.Accepted = true;
        //        return;
        //    }

        //    Client client = e.Item as Client;
        //    if (client.FirstName.ToUpper().Contains(FilterText.ToUpper()))
        //    {
        //        e.Accepted = true;
        //    }
        //    else
        //    {
        //        e.Accepted = false;
        //    }
        //}
    }
}
