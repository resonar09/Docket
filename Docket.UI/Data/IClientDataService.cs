using Docket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data
{
    public interface IClientDataService
    {
        Task<Client> GeByIdAsync(int clientId);
        Task SaveAsync(Client client);
    }
}
