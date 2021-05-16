using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineShop;

namespace WineShop
{
    public class WineShopContext : DbContext
    {
        public WineShopContext(DbContextOptions options) : base(options) { }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<WineShop.WineType> WineType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WinePackage>()
                .HasKey(vp => new { vp.PackageId, vp.WineId });

            modelBuilder.Entity<WinePackage>()
                .HasOne(vp => vp.Package)
                .WithMany(p => p.Wines)
                .HasForeignKey(vp => vp.PackageId);

            modelBuilder.Entity<WinePackage>()
                .HasOne(vp => vp.Wine)
                .WithMany(v => v.Packages)
                .HasForeignKey(vp => vp.WineId);

            modelBuilder.Entity<WineType>().HasData(
             new WineType { Id = 1, Name = "Chardonay", Description = "Dobro vino"},
             new WineType { Id = 2, Name = "Vino tipa 2", Description = "Dobro vino1" },
             new WineType { Id = 3, Name = "Vino tipa 3", Description = "Dobro vino2" });

            modelBuilder.Entity<PackageType>().HasData(
             new PackageType { Id = 1, Name = "Dobrotvorni" },
             new PackageType { Id = 2, Name = "Akcijski" },
             new PackageType { Id = 3, Name = "Promidzbeni" });
        }

       
    }

    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int PackageTypeId { get; set; }
        public PackageType PackageType { get; set; }
        public List<WinePackage> Wines { get; set; }
    }

    public class PackageType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Wine
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int WineTypeId { get; set; }
        public WineType WineType { get; set; }
        public List<WinePackage> Packages { get; set; }
    }

    public class WineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class WinePackage
    {
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public int WineId { get; set; }
        public Wine Wine { get; set; }

        public int Quantity { get; set; }
    }
}
