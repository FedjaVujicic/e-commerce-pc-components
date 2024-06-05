using ComponentShopAPI.Helpers;
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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(_context.Products.Select(product => new
            {
                product.Id,
                product.Name,
                product.Price,
                availability = product.Quantity > 0,
                ImageFile = _imageService.Download(product.ImageName, ProductType.Gpu)
            }));
        }

    }
}
