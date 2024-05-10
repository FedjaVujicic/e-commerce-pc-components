using ComponentShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;

        public MonitorsController(ComponentShopDBContext context)
        {
            _context = context;
        }

        // GET: api/Monitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Monitor>>> GetMonitors()
        {
            return Ok(await _context.Monitors.ToListAsync());
        }

        // GET: api/Monitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Monitor>> GetMonitor(int id)
        {
            var monitor = await _context.Monitors.FindAsync(id);

            if (monitor == null)
            {
                return NotFound();
            }

            return monitor;
        }

        // PUT: api/Monitors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonitor(int id, Models.Monitor monitor)
        {
            if (id != monitor.Id)
            {
                return BadRequest();
            }

            _context.Entry(monitor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonitorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.Monitors.ToListAsync());
        }

        // POST: api/Monitors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Monitor>> PostMonitor(Models.Monitor monitor)
        {
            _context.Monitors.Add(monitor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Monitors.ToListAsync());
        }

        // DELETE: api/Monitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitor(int id)
        {
            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor == null)
            {
                return NotFound();
            }

            _context.Monitors.Remove(monitor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Monitors.ToListAsync());
        }

        private bool MonitorExists(int id)
        {
            return _context.Monitors.Any(e => e.Id == id);
        }
    }
}
