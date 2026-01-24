using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repository
{
    public class GenericRepository<TEnity, TKey> : IGenericRepository<TEnity, TKey> where TEnity : BaseEnitiy<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEnity entity)=> await _dbContext.Set<TEnity>().AddAsync(entity);

        public async Task<IEnumerable<TEnity>> GetAllAsync() =>await _dbContext.Set<TEnity>().ToListAsync();


        public async Task<TEnity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEnity>().FindAsync(id);


        public void Remove(TEnity entity) => _dbContext.Set<TEnity>().Remove(entity);


        public void Update(TEnity entity) => _dbContext.Set<TEnity>().Update(entity);

    }
}
