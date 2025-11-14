using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Repositories;
using Web.Application.Services;
using Web.Domain.Entities.Catalog;
using Web.Persistence.Catalog;

namespace Web.Application.UnitTests
{    
    public class CatalogRepositoryTest
    {
        [Fact]
        public async Task Get_all_items_query_sucess() {
            var testContext = await TestingEntities.CreateTestCatalogContext();
            await TestingEntities.SeedTestCatalogContext(testContext);
            var testService = new CatalogRepositoryAsync(testContext);
            var countOfItemsSeed = 40;

            var result = testService.GetAllItemsQuery();

            var catalogItems = Assert.IsAssignableFrom<IQueryable<CatalogItem>>(result);
            Assert.Equal(countOfItemsSeed, catalogItems.Count());
        }

        //TODO: do another methods

    }   
}
