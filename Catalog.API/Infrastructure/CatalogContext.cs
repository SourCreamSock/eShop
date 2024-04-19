using Catalog.API.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext: DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
            var migrator = this.GetService<IMigrator>();
            migrator.Migrate("20240319172520_addEAV");
            //Database.EnsureDeleted();
            Database.Migrate();
            //    CatalogItems = catalogItems ?? throw new ArgumentNullException(nameof(catalogItems));
            //    CatalogTypes = catalogTypes ?? throw new ArgumentNullException(nameof(catalogTypes));            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogItemCategory>().ToTable("CatalogItemCategories").HasKey(k => k.Id);

            modelBuilder.Entity<CatalogItem>().ToTable("CatalogItems").HasKey(k => k.Id);
            modelBuilder.Entity<CatalogItem>().HasOne(o => o.CatalogItemCategory).WithMany().HasForeignKey(k => k.CategoryId)
                .HasPrincipalKey(k => k.Id);

            modelBuilder.Entity<CatalogItemAttribute>().ToTable("CatalogItemAttributes").HasKey(k => k.Id);                        

            modelBuilder.Entity<CatalogItemAttributeCategory>().ToTable("CatalogItemAttributeCategories");
            modelBuilder.Entity<CatalogItemAttributeCategory>().HasOne(o => o.CatalogItemAttribute).WithMany().HasForeignKey(k => k.CatalogItemAttributeId)
                .HasPrincipalKey(k => k.Id);
            modelBuilder.Entity<CatalogItemAttributeCategory>().HasOne(o => o.CatalogItemCategory).WithMany().HasForeignKey(k => k.CatalogItemCategoryId)
                .HasPrincipalKey(k => k.Id);   
            
            modelBuilder.Entity<CatalogItemAttributeValue>().ToTable("CatalogItemAttributeValues").HasKey(k => k.Id);
            modelBuilder.Entity<CatalogItemAttributeValue>().HasOne(o => o.CatalogItem).WithMany().HasForeignKey(k => k.CatalogItemId)
                .HasPrincipalKey(k => k.Id);
            modelBuilder.Entity<CatalogItemAttributeValue>().HasOne(o => o.CatalogItemAttribute).WithMany().HasForeignKey(k => k.CatalogItemAttributeId)
                .HasPrincipalKey(k => k.Id);


        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogItemCategory> CatalogItemCategories { get; set; }
        public DbSet<CatalogItemAttribute> CatalogItemAttributes { get; set; }        
        public DbSet<CatalogItemAttributeCategory> CatalogItemAttributeCategories { get; set; }        
        public DbSet<CatalogItemAttributeValue> CatalogItemAttributeValues { get; set; }        
    }
  

}

