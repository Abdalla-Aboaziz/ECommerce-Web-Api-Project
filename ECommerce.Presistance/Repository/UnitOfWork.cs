using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Presistance.Data.DBContexts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly Dictionary<Type, object> _repository =[];
        public UnitOfWork(StoreDbContext dbContext)
        {
          _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEnitiy<TKey>
        {
            var entityType = typeof(TEntity);
            if (_repository.TryGetValue(entityType, out object? repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }

            var newRepository = new GenericRepository<TEntity, TKey>(_dbContext);
            _repository[entityType] = newRepository;
            return newRepository;

        }

        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();

    }
}
