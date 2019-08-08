using Docket.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Docket.UI.ViewModel
{
    public interface INavigationViewModel
    {
        Task LoadAsync();
        //ObservableCollection<LookupItem> Clients { get; }

    }
}