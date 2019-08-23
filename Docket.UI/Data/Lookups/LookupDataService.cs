using Docket.DataAccess;
using Docket.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data.Lookups
{
    public class LookupDataService : IClientLookupDataService, IProgrammingLanguageLookupDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public LookupDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<IEnumerable<LookupItem>> GetClientLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Clients.AsNoTracking().Select(f=> new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.FirstName + " " + f.LastName
                }).ToListAsync();
            }
        }
        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var proLangs = await ctx.ProgrammingLanguages.AsNoTracking().Select(f => new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.Name
                }).ToListAsync();
                return proLangs;
            }

               
        }
    }
}
