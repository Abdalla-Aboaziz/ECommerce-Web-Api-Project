using ECommerce.ServiceAbstraction;
using ECommerce.Shared.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        // Get All Products

        [HttpGet]
        // GET: api/Product
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts(int? brandId,int? typeId)
        {
            var products = await _productServices.GetAllProductsAsync(brandId,typeId);
            return Ok(products);
        }

        // Get Product By Id

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProducts(int id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        // Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetAllBrands()
        {
            var brands = await _productServices.GetAllBrandsAsync();
            return Ok(brands);
        }
        // Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetAllTypes()
        {
            var types = await _productServices.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
