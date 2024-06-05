using ComponentShopAPI.Dtos;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;
using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly IImageService _imageService;

        public ProductsController(ComponentShopDBContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var productDtos = _context.Products.Select(product => new ProductDto(product, _imageService)).ToList();
            return Ok(productDtos);
        }

    }
}
