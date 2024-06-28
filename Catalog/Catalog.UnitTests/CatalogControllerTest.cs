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
    public class CatalogControllerTest : IClassFixture<DbCatalogContextFixture>
    {
        private readonly CatalogContext _catalogContext;
        private readonly IMapper _mapper;
        public CatalogControllerTest(DbCatalogContextFixture dbCatalogContextFixture)
        {
            _catalogContext = dbCatalogContextFixture.CatalogContext;

            var defaultProfile = new DefaultAutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(defaultProfile));
            _mapper = new Mapper(configuration);
        }

        [Theory]
        [InlineData(1, 1, 10, 0, 20)]
        public async Task Get_catalog_items_success(long categoryId, long brandId, int pageSize, int pageIndex, int expectedTotalCount)
        {
            var pictureHelperMock = new Mock<IPictureHelper>();
            pictureHelperMock.Setup(s => s.FullPathToPicture(It.IsAny<string>())).Returns<string>(value => value);
            var testCatalogController = new CatalogController(_catalogContext, pictureHelperMock.Object, _mapper);

            var actionResult = await testCatalogController.ItemsAsync(categoryId, brandId, pageSize, pageIndex);

            Assert.NotNull(actionResult);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            var catalogItemsResponse = Assert.IsAssignableFrom<CatalogItemsResponse>(okObjectResult.Value);
            Assert.Equal(catalogItemsResponse.TotalCount, expectedTotalCount);
        }
    }
    public class DbCatalogContextFixture : IAsyncLifetime
    {
        public async Task DisposeAsync()
        {
            await CatalogContext.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
                   .UseInMemoryDatabase("testDataBase");
            var options = optionsBuilder.Options;

            CatalogContext = new CatalogContext(options, isUseMigrations: false);
            var catalogContextSeed = new CatalogContextSeed();
            await catalogContextSeed.SeedAsync(CatalogContext);
        }
        public CatalogContext CatalogContext { get; set; }
    }

}