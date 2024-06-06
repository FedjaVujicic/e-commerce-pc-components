using ComponentShopAPI.Dtos;
using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.ProductDtoFactory;
using ComponentShopAPI.Services.Search;
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

        public ProductsController(ComponentShopDBContext context, IImageService imageService,
            IProductDtoFactory productDtoFactory, ISearchService searchService)
        {
            _context = context;
            _imageService = imageService;
            _productDtoFactory = productDtoFactory;
            _searchService = searchService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] ProductQueryParameters productQueryParameters)
        {
            IEnumerable<Product> products = productQueryParameters.Category switch
            {
                "Monitor" => _context.Monitors,
                "Gpu" => _context.Gpus,
                "" => _context.Products,
                _ => throw new BadHttpRequestException($"Invalid category {productQueryParameters.Category}")
            };
            products = _searchService.Search(products.ToList(), productQueryParameters);

            var productDtos = products.ToList().Select(_productDtoFactory.Create).ToList();
            return Ok(productDtos);
        }
    }
}
