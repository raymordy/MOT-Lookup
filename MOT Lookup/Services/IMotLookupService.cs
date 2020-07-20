using MOT_Lookup.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MOT_Lookup.Services
{
    interface IMotLookupService
    {
        Task<Vehicle> CreateApiRequestAsync(string registration);
    }
}
