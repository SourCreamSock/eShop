using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Persistence.Catalog
{
    public static class ServiceExtensions
    {
        public static void AddCatalogContext(this IServiceCollection collection, string catalogDbConnectionString)
        {
            collection.AddDbContext<CatalogContext>(options => options.UseSqlServer(catalogDbConnectionString));         
        }
        public static void MigrateCatalogDatabase(this IServiceProvider serviceProvider)
        {
            var dbCatalogContextOptions = serviceProvider.GetRequiredService<DbContextOptions<CatalogContext>>();
            using(var catalogContext = new CatalogContext(dbCatalogContextOptions))
            {
                catalogContext.Database.Migrate();
            }            
        }
        public static void Add
    }
}
