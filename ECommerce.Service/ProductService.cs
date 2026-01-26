using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModules;
using ECommerce.Service.Specifications;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
          _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            // mapping from ProductBrand to ProductBrandDTO
            return _mapper.Map<IEnumerable<ProductBrandDTO>>(brands);

        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var spec = new ProductWithBrandsAndTypeSpecification();
          var products =await  _unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);
            // mapping from Product to ProductDTO
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
           var types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            // mapping from ProductType to ProductTypeDTO
            return _mapper.Map<IEnumerable<ProductTypeDTO>>(types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypeSpecification(id);

            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(spec);
            // mapping from Product to ProductDTO
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
