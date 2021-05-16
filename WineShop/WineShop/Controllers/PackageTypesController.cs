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
    public class PackageTypesController : ControllerBase
    {
        private readonly IDbContextFactory<WineShopContext> _contextFactory;

        public PackageTypesController(IDbContextFactory<WineShopContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/PackageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageType>>> GetPackageTypes()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.PackageTypes.ToListAsync();
        }

        // GET: api/PackageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageType>> GetPackageType(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var packageType = await context.PackageTypes.FindAsync(id);

            if (packageType == null)
            {
                return NotFound();
            }

            return packageType;
        }

        // PUT: api/PackageTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackageType(int id, PackageType packageType)
        {
            var context = _contextFactory.CreateDbContext();
            if (id != packageType.Id)
            {
                return BadRequest();
            }

            context.Entry(packageType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageTypeExists(id))
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

        // POST: api/PackageTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PackageType>> PostPackageType(PackageType packageType)
        {
            var context = _contextFactory.CreateDbContext();
            context.PackageTypes.Add(packageType);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPackageType", new { id = packageType.Id }, packageType);
        }

        // DELETE: api/PackageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageType(int id)
        {
            var context = _contextFactory.CreateDbContext();

            var packageType = await context.PackageTypes.FindAsync(id);
            if (packageType == null)
            {
                return NotFound();
            }

            context.PackageTypes.Remove(packageType);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageTypeExists(int id)
        {
            var context = _contextFactory.CreateDbContext();
            return context.PackageTypes.Any(e => e.Id == id);
        }
    }
}
