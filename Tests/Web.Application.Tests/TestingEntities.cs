using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Persistence.Catalog;

namespace Web.Application.UnitTests
{
    public static class TestingEntities
    {
        public static async Task<CatalogContext> CreateTestCatalogContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                      .UseInMemoryDatabase("testDataBase");
            var options = optionsBuilder.Options;
            var catalogContext = new CatalogContext(options);
            catalogContext.Database.EnsureDeleted();            
            return catalogContext;
        }
        public static async Task SeedTestCatalogContext(CatalogContext catalogContext)
        {            
            var catalogContextSeed = new CatalogContextSeed();
            await catalogContextSeed.SeedAsync(catalogContext);            
        }
    }
}
