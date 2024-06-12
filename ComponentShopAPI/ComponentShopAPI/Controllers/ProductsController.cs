using ComponentShopAPI.Dtos;
using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.Pagination;
using ComponentShopAPI.Services.ProductDtoFactory;
using ComponentShopAPI.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly IImageService _imageService;
        private readonly IProductDtoFactory _productDtoFactory;
        private readonly ISearchService _searchService;
        private readonly IPaginationService _paginationService;

        public ProductsController(ComponentShopDBContext context, IImageService imageService,
            IProductDtoFactory productDtoFactory, ISearchService searchService, IPaginationService paginationService)
        {
            _context = context;
            _imageService = imageService;
            _productDtoFactory = productDtoFactory;
            _searchService = searchService;
            _paginationService = paginationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] ProductQueryParameters productQueryParameters)
        {
            IEnumerable<Product> products = productQueryParameters.Category switch
            {
                "monitor" => _context.Monitors,
                "gpu" => _context.Gpus,
                "" => _context.Products,
                _ => throw new BadHttpRequestException($"Invalid category {productQueryParameters.Category}")
            };
            products = _searchService.Search(products.ToList(), productQueryParameters);

            Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Append("X-Total-Count", products.Count().ToString());

            products = _paginationService.Paginate(products.ToList(), productQueryParameters.CurrentPage, productQueryParameters.PageSize);

            var productDtos = products.ToList().Select(_productDtoFactory.Create).ToList();
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_productDtoFactory.Create(product));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (product.ImageName != null)
            {
                _imageService.DeleteIfExists(product.ImageName);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
