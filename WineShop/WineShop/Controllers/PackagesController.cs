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
    public class PackagesController : ControllerBase
    {
        private readonly IDbContextFactory<WineShopContext> _contextFactory;

        public PackagesController(IDbContextFactory<WineShopContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // GET: api/Packages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Packages.Include(p => p.PackageType).ToListAsync();
        }

        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var package = await context.Packages.Include(p => p.PackageType).FirstOrDefaultAsync(p => p.Id == id);

            if (package == null)
            {
                return NotFound();
            }

            return package;
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            var context = _contextFactory.CreateDbContext();

            if (id != package.Id)
            {
                return BadRequest();
            }

            context.Entry(package).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            var context = _contextFactory.CreateDbContext();

            context.Packages.Add(package);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.Id }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var context = _contextFactory.CreateDbContext();

            var package = await context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            context.Packages.Remove(package);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            var context = _contextFactory.CreateDbContext();

            return context.Packages.Any(e => e.Id == id);
        }


        // GET: api/Packages/5/wines
        [HttpGet("{id}/wines")]
        public async Task<ActionResult<List<WinePackage>>> GetPackageWines(int id)
        {
            var context = _contextFactory.CreateDbContext();
            var wines = await context.Packages
                .Where(p => p.Id == id)
                .SelectMany(p => p.Wines)
                .Include(pw => pw.Wine)
                .ToListAsync();

            return wines;
        }

        // PUT: api/Packages/5/addwine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/addwine")]
        public async Task<IActionResult> AddWineToPackage(int id, WinePackage winePackage)
        {
            var context = _contextFactory.CreateDbContext();

            if (id != winePackage.PackageId)
            {
                return BadRequest();
            }

            var packageToUpdate = context.Packages.Include(p => p.Wines).First(p => p.Id == id);
            packageToUpdate.Wines.Add(winePackage);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        [HttpPut("{id}/removewine")]
        public async Task<IActionResult> RemoveWineFromPackage(int id, WinePackage winePackage)
        {
            var context = _contextFactory.CreateDbContext();

            if (id != winePackage.PackageId)
            {
                return BadRequest();
            }

            var packageToUpdate = context.Packages.Include(p => p.Wines).First(p => p.Id == id);
            var wineToRemove = packageToUpdate.Wines.First(pw => pw.WineId == winePackage.WineId);

            packageToUpdate.Wines.Remove(wineToRemove);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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

        [HttpPut("{id}/updatewine")]
        public async Task<IActionResult> UpdateWineInPackage(int id, WinePackage winePackage)
        {
            var context = _contextFactory.CreateDbContext();

            if (id != winePackage.PackageId)
            {
                return BadRequest();
            }

            var packageToUpdate = context.Packages.Include(p => p.Wines).First(p => p.Id == id);
            var wineToUpdate = packageToUpdate.Wines.First(pw => pw.WineId == winePackage.WineId);
            wineToUpdate.Quantity = winePackage.Quantity;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
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
    }
}
