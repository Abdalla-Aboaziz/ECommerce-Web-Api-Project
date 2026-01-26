using ECommerce.Domain.Entities.ProductModules;

namespace ECommerce.Service.Specifications
{
    public class ProductWithBrandsAndTypeSpecification : BaseSpecification<Product, int>
    {
        //This specification is used to include the ProductBrand and ProductType navigation properties

        // (Get Product By Id)
        public ProductWithBrandsAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
        //  (Get All Products)
        public ProductWithBrandsAndTypeSpecification(int? brandId, int? typeId) : base(p => (!brandId.HasValue|| p.BrandId == brandId.Value) && (typeId == null || p.TypeId == typeId))
        {
            //brandid is not null
            // typeid is not null
            // typeid and brandid is not null
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
