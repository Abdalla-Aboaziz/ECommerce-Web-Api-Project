using ECommerce.Domain.Entities.ProductModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductWithBrandsAndTypeSpecification : BaseSpecification<Product,int>
    {
        //This specification is used to include the ProductBrand and ProductType navigation properties

        // (Get Product By Id)
        public ProductWithBrandsAndTypeSpecification(int id ) :base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
      //  (Get All Products)
        public ProductWithBrandsAndTypeSpecification():base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
