using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEnitiy<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get;  }
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria= criteria;
        }
        #region Include
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        } 
        #endregion
    }
}