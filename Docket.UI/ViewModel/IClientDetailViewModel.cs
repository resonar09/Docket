using System.Threading.Tasks;
using Docket.Model;

namespace Docket.UI.ViewModel
{
    public interface IClientDetailViewModel
    {
        //Client Client { get; }

        Task LoadAsync(int clientId);
    }
}