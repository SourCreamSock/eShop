using AutoMapper;
using Catalog.API.Controllers;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.AutoMapperProfiles;
using Catalog.API.Model.API_Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Catalog.UnitTests
{
    public class CatalogControllerTest
    {
        //private readonly CatalogContext _catalogContext;
        //private readonly CatalogController _testCatalogController;
        public CatalogControllerTest()
        {
            //_catalogContext = dbCatalogContextFixture.CatalogContext;

            //var defaultProfile = new DefaultAutoMapperProfile();
            //var configuration = new MapperConfiguration(cfg => cfg.AddProfile(defaultProfile));
            //var mapper = new Mapper(configuration);

            //var pictureHelperMock = new Mock<IPictureHelper>();
            //pictureHelperMock.Setup(s => s.FullPathToPicture(It.IsAny<string>())).Returns<string>(value => value);

            //_testCatalogController = new CatalogController(_catalogContext, pictureHelperMock.Object, mapper);
        }
        [Theory]
        [InlineData(1L)]
        public async Task Delete_catalog_items_success(long itemId)
        {
            var testEntites = await TestingEntities.CreateTestingEntities();
            var testCatalogController = testEntites.TestCatalogController;            
        }

        //[Theory]
        //[InlineData(1L, 1L, 10, 0, 20L)]
        //[InlineData(null, null, 40, 0, 40L)]
        //public async Task Get_catalog_items_success(long? categoryId, long? brandId, int pageSize, int pageIndex, long expectedTotalCount)
        //{
        //    var actionResult = await _testCatalogController.ItemsAsync(categoryId, brandId, pageSize, pageIndex);

        //    Assert.NotNull(actionResult);
        //    var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
        //    var catalogItemsResponse = Assert.IsAssignableFrom<CatalogItemsResponse>(okObjectResult.Value);
        //    Assert.Equal(expectedTotalCount, catalogItemsResponse.TotalCount);
        //}`
        [Theory]
        [InlineData(1L, 1L, 10, 0, 20L)]
        [InlineData(null, null, 40, 0, 40L)]
        public async Task Get_catalog_items_success(long? categoryId, long? brandId, int pageSize, int pageIndex, long expectedTotalCount)
        {
            var testEntites = await TestingEntities.CreateTestingEntities();
            var testCatalogController = testEntites.TestCatalogController;
            var actionResult = await testCatalogController.ItemsAsync(new CatalogController.ItemFilter(categoryId, brandId, pageSize, pageIndex));

            Assert.NotNull(actionResult);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            var catalogItemsResponse = Assert.IsAssignableFrom<CatalogItemsResponse>(okObjectResult.Value);
            Assert.Equal(expectedTotalCount, catalogItemsResponse.TotalCount);
        }
        [Theory]
        [InlineData(-1L, -1L, -1, -1)]
        public async Task Get_catalog_items_with_wrong_parameters_is_badrequest(long? categoryId, long? brandId, int pageSize, int pageIndex)
        {
            var testEntites = await TestingEntities.CreateTestingEntities();
            var testCatalogController = testEntites.TestCatalogController;
            var actionResult = await testCatalogController.ItemsAsync(new CatalogController.ItemFilter(categoryId, brandId, pageSize, pageIndex));

            Assert.IsType<BadRequestResult>(actionResult);
        }
        
    }
    public class TestingEntities
    {
        public  CatalogContext CatalogContext { get; set; }
        public CatalogController TestCatalogController { get; set; }
        private TestingEntities(CatalogContext catalogContext, CatalogController catalogController) {
            CatalogContext = catalogContext;
            TestCatalogController = catalogController;
        }
        public async static Task<TestingEntities> CreateTestingEntities()
        {
            var catalogContext = await CreateTestCatalogContext();
            var catalogController = await CreateCatalogControllerTest(catalogContext);
            return new TestingEntities(catalogContext, catalogController);
        }
        public static async Task<CatalogContext> CreateTestCatalogContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                      .UseInMemoryDatabase("testDataBase");
            var options = optionsBuilder.Options;
            var dbContextCustomSettings = new DbContextCustomSettings { IsUseMigrations = false };

            var catalogContext = new CatalogContext(options, dbContextCustomSettings);
            catalogContext.Database.EnsureDeleted();
            var catalogContextSeed = new CatalogContextSeed();
            await catalogContextSeed.SeedAsync(catalogContext);
            return catalogContext;
        }
        public static async Task<CatalogController> CreateCatalogControllerTest(CatalogContext catalogContext)
        {
            var defaultProfile = new DefaultAutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(defaultProfile));
            var mapper = new Mapper(configuration);

            var pictureHelperMock = new Mock<IPictureHelper>();
            pictureHelperMock.Setup(s => s.FullPathToPicture(It.IsAny<string>())).Returns<string>(value => value);

            var testCatalogController = new CatalogController(catalogContext, pictureHelperMock.Object, mapper);
            return testCatalogController;
        }
        //~TestingEntities()
        //{
        //    CatalogContext.Dispose();
        //}
        //public void Dispose()
        //{
        //    CatalogContext.Dispose();
        //}
    }
    
    //public class DbCatalogContextFixture : IAsyncLifetime
    //{
    //    public async Task DisposeAsync()
    //    {
    //        await CatalogContext.DisposeAsync();
    //    }

    //    public async Task InitializeAsync()
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
    //               .UseInMemoryDatabase("testDataBase");
    //        var options = optionsBuilder.Options;

    //        CatalogContext = new CatalogContext(options, isUseMigrations: false);
    //        var catalogContextSeed = new CatalogContextSeed();
    //        await catalogContextSeed.SeedAsync(CatalogContext);
    //    }
    //    public CatalogContext CatalogContext { get; set; }
    //}

}