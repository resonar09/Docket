using Docket.DataAccess;
using Docket.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data
{
    public class ClientDataService : IClientDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public ClientDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Client> GeByIdAsync(int clientId)
        {
            using (var ctx = new DocketDbContext())
            {
                return await ctx.Clients.AsNoTracking().SingleAsync(c=>c.Id == clientId);
            }
        }

        public async Task SaveAsync(Client client)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Clients.Attach(client);
                ctx.Entry(client).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
