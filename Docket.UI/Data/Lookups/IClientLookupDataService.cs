﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Docket.Model;

namespace Docket.UI.Data.Lookups
{
    public interface IClientLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetClientLookupAsync();
    }
}