using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Monitor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly IMonitorService _monitorService;

        public MonitorsController(ComponentShopDBContext context, IMonitorService monitorService)
        {
            _context = context;
            _monitorService = monitorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Monitor>> GetMonitors
            ([FromQuery] MonitorQueryParameters queryParameters)
        {
            var monitors = _monitorService.Search(_context.Monitors.ToList(), queryParameters);

            Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
            Response.Headers.Append("X-Total-Count", monitors.Count.ToString());

            if (monitors.Count == 0)
            {
                return Ok();
            }

            return Ok(_monitorService.Paginate(monitors, queryParameters));
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
        [Authorize(Roles = "Admin")]
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

            return NoContent();
        }

        // POST: api/Monitors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Models.Monitor>> PostMonitor(Models.Monitor monitor)
        {
            _context.Monitors.Add(monitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonitor", new { id = monitor.Id }, monitor);
        }

        // DELETE: api/Monitors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMonitor(int id)
        {
            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor == null)
            {
                return NotFound();
            }

            _context.Monitors.Remove(monitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("supportedProperties")]
        public ActionResult<IEnumerable<string>> GetSupportedProperties()
        {
            return Ok(
                new
                {
                    resolutions = _context.Monitors.Select(monitor => $"{monitor.Width}x{monitor.Height}").Distinct(),
                    refreshRates = _context.Monitors.Select(monitor => monitor.RefreshRate).Distinct()
                }
            );
        }

        private bool MonitorExists(int id)
        {
            return _context.Monitors.Any(e => e.Id == id);
        }
    }
}
