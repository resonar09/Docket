using Docket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data.Repository
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int clientId);
        Task SaveAsync();
        bool HasChanges();
        void Add(Client client);
    }
}
