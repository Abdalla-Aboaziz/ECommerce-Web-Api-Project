using ECommerce.Domain.Contracts;
using ECommerce.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class CashService : ICashService
    {
        private readonly ICachRepository _cachRepository;

        public CashService(ICachRepository cachRepository)
        {
            _cachRepository = cachRepository;
        }
        public async Task<string?> GetAsync(string key)
        {
          return await _cachRepository.GetAsync(key);
        }

        public async Task SetAsync(string Cachkey, object Cachvalue, TimeSpan timeToLive)
        {
           var value=JsonSerializer.Serialize(Cachvalue);
          await _cachRepository.SetAsync(Cachkey, value, timeToLive);
        }
    }
}
