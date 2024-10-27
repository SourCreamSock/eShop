using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext: DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options, DbContextCustomSettings dbContextCustomSettings) : base(options)
        {
            //Database.EnsureCreated();
            if(dbContextCustomSettings.IsUseMigrations)
                Database.Migrate();
            //    CatalogItems = catalogItems ?? throw new ArgumentNullException(nameof(catalogItems));
            //    CatalogTypes = catalogTypes ?? throw new ArgumentNullException(nameof(catalogTypes));            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogBrand>().ToTable("CatalogBrands").HasKey(k => k.Id);
            modelBuilder.Entity<CatalogCategory>().ToTable("CatalogCategories").HasKey(k => k.Id);

            modelBuilder.Entity<CatalogItem>().ToTable("CatalogItems").HasKey(k => k.Id);
            modelBuilder.Entity<CatalogItem>().HasOne(o => o.CatalogBrand).WithMany().HasForeignKey(k => k.CatalogBrandId)
                .HasPrincipalKey(k => k.Id);
            modelBuilder.Entity<CatalogItem>().HasOne(o => o.CatalogCategory).WithMany().HasForeignKey(k => k.CatalogCategoryId)
                .HasPrincipalKey(k => k.Id);
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogCategory> CatalogCategories { get; set; }
               
    }
  

}

