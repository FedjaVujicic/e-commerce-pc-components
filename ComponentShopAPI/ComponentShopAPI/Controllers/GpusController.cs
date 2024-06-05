using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Gpu;
using ComponentShopAPI.Services.Image;
using ComponentShopAPI.Services.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpusController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly IGpuService _gpuService;
        private readonly IImageService _imageService;
        private readonly IPaginationService _paginationService;

        public GpusController(ComponentShopDBContext context, IGpuService gpuService, IImageService imageService,
            IPaginationService paginationService)
        {
            _context = context;
            _gpuService = gpuService;
            _imageService = imageService;
            _paginationService = paginationService;
        }

        [HttpGet]
        public ActionResult GetGpus
            ([FromQuery] GpuQueryParameters queryParameters)
        {
            var gpus = _gpuService.Search(_context.Gpus.ToList(), queryParameters);


            Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Append("X-Total-Count", gpus.Count.ToString());

            if (gpus.Count == 0)
            {
                return Ok();
            }

            gpus = _paginationService.Paginate(gpus, queryParameters.CurrentPage, queryParameters.PageSize);

            return Ok(gpus.Select(gpu => new
            {
                gpu.Id,
                gpu.Name,
                gpu.Price,
                availability = gpu.Quantity > 0,
                gpu.Slot,
                gpu.Memory,
                gpu.Ports,
                gpu.ImageName,
                ImageFile = _imageService.Download(gpu.ImageName, ProductType.Gpu)
            }));
        }


        // GET: api/Gpus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gpu>> GetGpu(int id)
        {
            var gpu = await _context.Gpus.FindAsync(id);

            if (gpu == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                gpu.Id,
                gpu.Name,
                gpu.Price,
                availability = gpu.Quantity > 0,
                gpu.Slot,
                gpu.Memory,
                gpu.Ports,
                gpu.ImageName,
                ImageFile = _imageService.Download(gpu.ImageName, ProductType.Gpu)
            });
        }

        // PUT: api/Gpus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutGpu(int id, Gpu gpu)
        {
            if (id != gpu.Id)
            {
                return BadRequest();
            }

            var oldGpu = await _context.Gpus.FindAsync(gpu.Id);
            if (oldGpu == null)
            {
                return NotFound();
            }
            _imageService.DeleteIfExists(oldGpu.ImageName, ProductType.Gpu);

            _context.Entry(oldGpu).State = EntityState.Detached;

            if (gpu.ImageFile != null)
            {
                gpu.ImageName = await _imageService.Upload(gpu.ImageFile, ProductType.Gpu);
            }

            _context.Entry(gpu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GpuExists(id))
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

        // POST: api/Gpus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Gpu>> PostGpu(Gpu gpu)
        {
            if (gpu.ImageFile != null)
            {
                gpu.ImageName = await _imageService.Upload(gpu.ImageFile, ProductType.Gpu);
            }

            _context.Gpus.Add(gpu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGpu", new { id = gpu.Id }, gpu);
        }

        // DELETE: api/Gpus/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGpu(int id)
        {
            var gpu = await _context.Gpus.FindAsync(id);
            if (gpu == null)
            {
                return NotFound();
            }

            if (gpu.ImageName != null)
            {
                _imageService.DeleteIfExists(gpu.ImageName, ProductType.Gpu);
            }

            _context.Gpus.Remove(gpu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("supportedProperties")]
        public ActionResult<IEnumerable<string>> GetSupportedProperties()
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

        [HttpGet("{id}/quantity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetQuantity(int id)
        {
            var gpu = await _context.Gpus.FindAsync(id);
            if (gpu == null)
            {
                return NotFound();
            }
            return Ok(gpu.Quantity);
        }

        private bool GpuExists(int id)
        {
            return _context.Gpus.Any(e => e.Id == id);
        }
    }
}
