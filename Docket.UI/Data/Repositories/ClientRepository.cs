using Docket.DataAccess;
using Docket.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DocketDbContext _context;

        public ClientRepository(DocketDbContext context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
        }

        public async Task<Client> GetByIdAsync(int clientId)
        {
            return await _context.Clients.SingleAsync(c => c.Id == clientId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Client model)
        {
            _context.Clients.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}