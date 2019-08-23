using System.Collections.Generic;
using System.Threading.Tasks;
using Docket.Model;

namespace Docket.UI.Data.Lookups
{
    public interface IProgrammingLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}