using ECommerce.Domain.Entities.ProductModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductWithBrandsAndTypes: BaseSpecification<Product,int>
    {
        public ProductWithBrandsAndTypes():base()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
