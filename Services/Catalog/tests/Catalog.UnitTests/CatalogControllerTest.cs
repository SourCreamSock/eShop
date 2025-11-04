using AutoMapper;
using Catalog.API.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Mappers.AutoMapperProfiles;
using Web.Application.Validatiors;
using Web.Domain.Entities.Catalog;
using Web.Persistence.Catalog;

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
            var catalogServiceMock = new Mock<ICatalogService>();
            catalogServiceMock.Setup(s => s.DeleteItem(It.IsAny<long>())).Returns(Task.CompletedTask);
            var testCatalogController = await TestingEntities.CreateCatalogControllerTest(catalogServiceMock);            
            var actionResult = await testCatalogController.DeleteItemAsync(itemId);

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<OkResult>(actionResult);
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
        [InlineData(1L, 1L, 10, 0, 2L)]
        [InlineData(null, null, 40, 1, 2L)]
        public async Task Get_catalog_items_success(long? categoryId, long? brandId, int pageSize, int pageIndex, long expectedTotalCount)
        {
            var catalogServiceMock = new Mock<ICatalogService>();
            var items = new List<CatalogItemResponseDto> {
                new CatalogItemResponseDto {
                    CatalogCategoryId = 1,
                    CatalogBrandId = 1,
                    Code ="ApplesAntonovka",
                    Name="Антоновка",
                    PictureUri = "ApplesAntonovka",
                    Description = "Вкусные  яблоки из антоновки. Описание Описание Описание " +
                        "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                },
                new CatalogItemResponseDto{
                    CatalogCategoryId= 1,
                    CatalogBrandId = 1,
                    Code="ApplesGreen",
                    Name="Зеленые яблоки",
                    PictureUri = "ApplesGreen",
                    Description = "Вкусные зеленые яблоки просто яблоки. Описание Описание Описание " +
                        "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                }
            };
            catalogServiceMock.Setup(s => s.GetItemsAsync(It.IsAny<GetItemsFilterDto>())).Returns(Task.FromResult<GetCatalogItemsResponseDto>(
                new GetCatalogItemsResponseDto
                {
                    CatalogItems = items,
                    TotalCount = items.Count
                }
            ));
            var testCatalogController = await TestingEntities.CreateCatalogControllerTest(catalogServiceMock);            
            var actionResult = await testCatalogController.ItemsAsync(new GetItemsFilterDto { CategoryId = categoryId, BrandId = brandId, PageSize = pageSize, PageIndex = pageIndex });

            Assert.NotNull(actionResult);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            var catalogItemsResponse = Assert.IsAssignableFrom<GetCatalogItemsResponseDto>(okObjectResult.Value);
            Assert.Equal(expectedTotalCount, catalogItemsResponse.TotalCount);
        }
        [Theory]
        [InlineData(-1L, 0L, 0, 0)]
        [InlineData(0L, -1L, 0, 0)]        
        [InlineData(0L, 0L, -1, 0)]
        [InlineData(0L, 0L, 0, -1)]
        public async Task Get_catalog_items_with_wrong_parameters_is_badrequest(long? categoryId, long? brandId, int pageSize, int pageIndex)
        {
            var catalogServiceMock = new Mock<ICatalogService>();
            //catalogServiceMock.Setup(s => s.GetItemsAsync(It.IsAny<GetItemsFilterDto>())).Returns<Task<CatalogItemsResponseDto>>(value => value);

            var testCatalogController = await TestingEntities.CreateCatalogControllerTest(catalogServiceMock);            
            var actionResult = await testCatalogController.ItemsAsync(new GetItemsFilterDto{ CategoryId = categoryId, BrandId = brandId, PageSize = pageSize, PageIndex = pageIndex });

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

    }
    public static class TestingEntities
    {
        public static async Task<CatalogController> CreateCatalogControllerTest(Mock<ICatalogService> catalogServiceMock)
        {
            var defaultProfile = new DefaultAutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(defaultProfile));
            var mapper = new Mapper(configuration);
            IValidator<GetItemsFilterDto> getItemsFilterDtoValidator = new GetItemsFilterDtoValidator();
            var testCatalogController = new CatalogController(catalogServiceMock.Object, mapper, getItemsFilterDtoValidator);            
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