using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistance
{
    public static class SpecificationEvaluator
    {
        // Create methods to evaluate specifications here in the future (Create Quary)
        // _dbcontext.products.Include(p=>p.BrandId).Include(p=>p.TypeId)

        public static IQueryable<TEnity> CreateQuery<TEnity,TKey>(IQueryable<TEnity> EntireyPoint,ISpecification<TEnity,TKey> specification) where TEnity : BaseEnitiy<TKey>
        {
            var Query = EntireyPoint;
            if (specification is not null)
            {
                if (specification.Criteria is not null)
                {
                    Query = Query.Where(specification.Criteria);
                }
                if (specification.IncludeExpression is not null && specification.IncludeExpression.Any())
                {
                    //foreach (var includeExpression in specification.IncludeExpression)
                    //{
                    //    Query = Query.Include(includeExpression);
                    //}

                    Query = specification.IncludeExpression
                        .Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));
                }
            }
            return Query;
        }

    }
}
