using ComponentShopAPI.Dtos;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.ProductDtoFactory;
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

        public ProductsController(ComponentShopDBContext context, IImageService imageService,
            IProductDtoFactory productDtoFactory)
        {
            _context = context;
            _imageService = imageService;
            _productDtoFactory = productDtoFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] string category = "")
        {
            IEnumerable<Product> products = category switch
            {
                "Monitor" => _context.Monitors,
                "Gpu" => _context.Gpus,
                "" => _context.Products,
                _ => throw new BadHttpRequestException($"Invalid category {category}")
            };

            var productDtos = products.ToList().Select(_productDtoFactory.Create).ToList();
            return Ok(productDtos);
        }
    }
}
