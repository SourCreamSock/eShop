namespace Catalog.API.Infrastructure
{
    public class CatalogContext: DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        //    CatalogItems = catalogItems ?? throw new ArgumentNullException(nameof(catalogItems));
        //    CatalogTypes = catalogTypes ?? throw new ArgumentNullException(nameof(catalogTypes));            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogType>().HasKey(k => k.Id);
            modelBuilder.Entity<CatalogItem>().ToTable("CatalogItems").HasKey(k => k.Id);
            modelBuilder.Entity<CatalogItem>().HasOne(o => o.CatalogType).WithMany().HasForeignKey(k => k.CatalogTypeId)
                .HasPrincipalKey(k => k.Id);
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }        
    }
  

}

