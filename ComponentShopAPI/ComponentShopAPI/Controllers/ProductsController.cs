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
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] string category = "")
        {
            if (category == "Monitor")
            {
                var monitorDtos = _context.Monitors.Select(monitor => new MonitorDto(monitor, _imageService)).ToList();
                return Ok(monitorDtos);
            }
            if (category == "Gpu")
            {
                var gpuDtos = _context.Gpus.Select(gpu => new GpuDto(gpu, _imageService)).ToList();
                return Ok(gpuDtos);
            }
            if (category == "")
            {
                var productDtos = _context.Products.Select(product => new ProductDto(product, _imageService)).ToList();
                return Ok(productDtos);
            }
            return BadRequest();
        }
    }
}
