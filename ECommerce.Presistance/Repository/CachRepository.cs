using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repository
{
    public class CachRepository : ICachRepository
    {
        private readonly IDatabase _database;
        public CachRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();

        }
        public async Task<string?> GetAsync(string Cachkey)
        {
           var cachValue = await _database.StringGetAsync(Cachkey);
            return cachValue.IsNullOrEmpty? null:cachValue.ToString();
        }

        public async Task SetAsync(string Cachkey, string CachValue, TimeSpan timetolive)
        {
            await _database.StringSetAsync(Cachkey, CachValue, timetolive);
        }
    }
}
