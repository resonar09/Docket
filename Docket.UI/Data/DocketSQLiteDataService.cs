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
    public class DocketSQLiteDataService : IDocketDataService
    {
        private readonly Func<DocketDbContext> _contextCreator;

        public DocketSQLiteDataService(Func<DocketDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public Task<List<Client>> GetAllAsync()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Client>();
                var clients = conn.Table<Client>().ToList();
                List<Client> clientList = new List<Client>();
                //return  Task.Run(()=> conn.Table<Client>().ToListAsync());
                foreach (var client in clients)
                {
                    clientList.Add(client);
                }
                return Task.Run(()=> clientList);
            }
        }

    }
}
