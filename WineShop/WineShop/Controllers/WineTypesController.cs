using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineShop;

namespace WineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineTypesController : ControllerBase
    {
        private readonly IDbContextFactory<WineShopContext> _contextFactory;

        public WineTypesController(IDbContextFactory<WineShopContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/WineTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WineType>>> GetWineType()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.WineType.ToListAsync();
        }

        // GET: api/WineTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WineType>> GetWineType(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var wineType = await context.WineType.FindAsync(id);

            if (wineType == null)
            {
                return NotFound();
            }

            return wineType;
        }

        // PUT: api/WineTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWineType(int id, WineType wineType)
        {
            if (id != wineType.Id)
            {
                return BadRequest();
            }

            var context = _contextFactory.CreateDbContext();
            context.Entry(wineType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WineTypeExists(id))
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

        // POST: api/WineTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WineType>> PostWineType(WineType wineType)
        {
            var context = _contextFactory.CreateDbContext();
            context.WineType.Add(wineType);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetWineType", new { id = wineType.Id }, wineType);
        }

        // DELETE: api/WineTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWineType(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var wineType = await context.WineType.FindAsync(id);
            if (wineType == null)
            {
                return NotFound();
            }

            context.WineType.Remove(wineType);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool WineTypeExists(int id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.WineType.Any(e => e.Id == id);
        }
    }
}
