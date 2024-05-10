using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComponentShopAPI.Models;

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

        // GET: api/Gpus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gpu>>> GetGraphicsCards()
        {
            return await _context.GraphicsCards.ToListAsync();
        }

        // GET: api/Gpus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gpu>> GetGpu(int id)
        {
            var gpu = await _context.GraphicsCards.FindAsync(id);

            if (gpu == null)
            {
                return NotFound();
            }

            return gpu;
        }

        // PUT: api/Gpus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGpu(int id, Gpu gpu)
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
        public async Task<ActionResult<Gpu>> PostGpu(Gpu gpu)
        {
            _context.GraphicsCards.Add(gpu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGpu", new { id = gpu.Id }, gpu);
        }

        // DELETE: api/Gpus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGpu(int id)
        {
            var gpu = await _context.GraphicsCards.FindAsync(id);
            if (gpu == null)
            {
                return NotFound();
            }

            _context.GraphicsCards.Remove(gpu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GpuExists(int id)
        {
            return _context.GraphicsCards.Any(e => e.Id == id);
        }
    }
}
