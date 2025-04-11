using Web.Domain.Entities.Catalog;

namespace Web.Persistence.Catalog
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {}
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

