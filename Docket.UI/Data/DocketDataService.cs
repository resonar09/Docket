using Docket.DataAccess;
using Docket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data
{
    public class DocketDataService : IDocketDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public DocketDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public Task<List<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
