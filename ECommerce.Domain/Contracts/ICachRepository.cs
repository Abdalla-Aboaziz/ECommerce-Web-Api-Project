using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ICachRepository
    {
        Task<string?> GetAsync(string Cachkey);
        Task SetAsync(string Cachkey, string CachValue, TimeSpan timetolive);

    }
}
