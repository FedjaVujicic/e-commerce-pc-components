using ComponentShopAPI.Dtos;
using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;
using ComponentShopAPI.Repositories;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.Pagination;
using ComponentShopAPI.Services.ProductDtoFactory;
using ComponentShopAPI.Services.ProductFactory;
using ComponentShopAPI.Services.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly IImageService _imageService;
        private readonly IProductDtoFactory _productDtoFactory;
        private readonly IProductFactory _productFactory;
        private readonly ISearchService _searchService;
        private readonly IPaginationService _paginationService;

        public ProductsController(ComponentShopDBContext context, IImageService imageService,
            IProductDtoFactory productDtoFactory, ISearchService searchService, IPaginationService paginationService,
            IProductFactory productFactory)
        {
            _context = context;
            _imageService = imageService;
            _productDtoFactory = productDtoFactory;
            _searchService = searchService;
            _paginationService = paginationService;
            _productFactory = productFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromQuery] ProductGetParameters queryParameters)
        {
            IEnumerable<Product> products = queryParameters.Category switch
            {
                "monitor" => _context.Monitors,
                "gpu" => _context.Gpus,
                "" => _context.Products,
                _ => throw new BadHttpRequestException($"Invalid category {queryParameters.Category}")
            };
            products = _searchService.Search(products.ToList(), queryParameters);

            Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Append("X-Total-Count", products.Count().ToString());

            products = _paginationService.Paginate(products.ToList(), queryParameters.CurrentPage, queryParameters.PageSize);

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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProduct(ProductPostParameters queryParameters)
        {
            var product = _productFactory.Create(queryParameters);

            if (product.ImageFile != null)
            {
                product.ImageName = await _imageService.Upload(product.ImageFile);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct(int id, ProductPostParameters queryParameters)
        {
            var product = _productFactory.Create(queryParameters);

            if (id != product.Id)
            {
                return BadRequest();
            }

            var oldProduct = await _context.Products.FindAsync(product.Id);
            if (oldProduct == null)
            {
                return NotFound();
            }
            _imageService.DeleteIfExists(oldProduct.ImageName);

            _context.Entry(oldProduct).State = EntityState.Detached;

            if (product.ImageFile != null)
            {
                product.ImageName = await _imageService.Upload(product.ImageFile);
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpGet("supportedProperties")]
        public ActionResult<IEnumerable<string>> GetSupportedProperties([FromQuery] string category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            if (category == "gpu")
            {
                return Ok(
                    new
                    {
                        slots = _context.Gpus.Select(gpu => gpu.Slot).Distinct(),
                        memories = _context.Gpus.Select(gpu => gpu.Memory).Distinct(),
                        ports = _context.Gpus.AsEnumerable().SelectMany(gpu => gpu.Ports).Distinct()
                    }
                );
            }
            if (category == "monitor")
            {
                return Ok(
                new
                {
                    resolutions = _context.Monitors.Select(monitor => $"{monitor.Width}x{monitor.Height}").Distinct(),
                    refreshRates = _context.Monitors.Select(monitor => monitor.RefreshRate).Distinct()
                }
                );
            }
            return BadRequest();
        }

        [HttpGet("{id}/quantity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetQuantity(int id)
        {
            var gpu = await _context.Products.FindAsync(id);
            if (gpu == null)
            {
                return NotFound();
            }
            return Ok(gpu.Quantity);
        }


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
