using Docket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Data
{
    public interface IDocketDataService
    {
        Task<List<Client>> GetAllAsync();
    }
}
