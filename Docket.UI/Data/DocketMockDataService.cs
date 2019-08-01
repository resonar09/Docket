using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docket.DataAccess;
using Docket.Model;

namespace Docket.UI.Data
{
    class DocketMockDataService : IDocketDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public DocketMockDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<List<Client>> GetAllAsync()
        {
            List<Client> list = new List<Client>();
            list.Add(new Client { FirstName = "Thomas", LastName = "Huber" });
            list.Add(new Client { FirstName = "Andreas", LastName = "Boehler" });
            list.Add(new Client { FirstName = "Julia", LastName = "Huber" });
            list.Add(new Client { FirstName = "Chris", LastName = "Egin" });
            //Mimic a long running get
            await Task.Delay(3000);
            //return Task.Run(() => list);
            return list;
        }
    }
}
