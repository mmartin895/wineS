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
    public class WinesController : ControllerBase
    {
        private readonly IDbContextFactory<WineShopContext> _contextFactory;
        public WinesController(IDbContextFactory<WineShopContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Wines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wine>>> GetWines()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Wines.ToListAsync();
        }

        // GET: api/Wines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wine>> GetWine(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var Wine = await context.Wines.FindAsync(id);

            if (Wine == null)
            {
                return NotFound();
            }

            return Wine;
        }

        // PUT: api/Wines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWine(int id, Wine Wine)
        {
            var context = _contextFactory.CreateDbContext();
            if (id != Wine.Id)
            {
                return BadRequest();
            }

            context.Entry(Wine).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WineExists(id))
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

        // POST: api/Wines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wine>> PostWine(Wine wine)
        {
            var context = _contextFactory.CreateDbContext();
            context.Wines.Add(wine);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetWine", new { id = wine.Id }, wine);
        }

        // DELETE: api/Wines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWine(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var Wine = await context.Wines.FindAsync(id);
            if (Wine == null)
            {
                return NotFound();
            }

            context.Wines.Remove(Wine);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool WineExists(int id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.Wines.Any(e => e.Id == id);
        }
    }
}
