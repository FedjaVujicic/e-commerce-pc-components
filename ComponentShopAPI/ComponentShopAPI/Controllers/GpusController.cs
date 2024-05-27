using ComponentShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpusController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;

        public GpusController(ComponentShopDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Gpu>>> GetGpus
            ([FromQuery] int currentPage, [FromQuery] int pageSize, [FromQuery] string searchParam = "")
        {
            var gpus = await _context.Gpus.ToListAsync();

            if (searchParam != "")
            {
                gpus = gpus.Where(gpu =>
                    gpu.Name.IndexOf(searchParam, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Append("X-Total-Count", gpus.Count.ToString());

            if (gpus.Count == 0)
            {
                return Ok();
            }

            if (gpus.Count >= currentPage * pageSize)
            {
                return Ok(gpus.GetRange((currentPage - 1) * pageSize, pageSize));
            }
            else
            {
                return Ok(gpus.GetRange((currentPage - 1) * pageSize, gpus.Count - ((currentPage - 1) * pageSize)));
            }
        }

        // GET: api/Gpus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Gpu>> GetGpu(int id)
        {
            var gpu = await _context.Gpus.FindAsync(id);

            if (gpu == null)
            {
                return NotFound();
            }

            return gpu;
        }

        // PUT: api/Gpus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGpu(int id, Models.Gpu gpu)
        {
            if (id != gpu.Id)
            {
                return BadRequest();
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
        public async Task<ActionResult<Models.Gpu>> PostGpu(Models.Gpu gpu)
        {
            _context.Gpus.Add(gpu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGpu", new { id = gpu.Id }, gpu);
        }

        // DELETE: api/Gpus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGpu(int id)
        {
            var gpu = await _context.Gpus.FindAsync(id);
            if (gpu == null)
            {
                return NotFound();
            }

            _context.Gpus.Remove(gpu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GpuExists(int id)
        {
            return _context.Gpus.Any(e => e.Id == id);
        }
    }
}
