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
    public class DocketSQLDataService : IDocketDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public DocketSQLDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<List<Client>> GetAllAsync()
        {
            using (var ctx = new DocketDbContext())
            {
                return await ctx.Clients.AsNoTracking().ToListAsync();
            }
        }
    }
}
