using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Mappers.AutoMapperProfiles;
using Web.Application.Services;
using Web.Domain.Entities.Catalog;
using Web.Domain.Repositories;
using Web.Persistence.Catalog;

namespace Web.Application.UnitTests
{
    public class CatalogServiceTets
    {
        [Theory]
        [InlineData(1L, 1L, 0, 2)]
        [InlineData(1L, 1L, 1, 1)]
        public async Task Get_Item_Success(long? brandId, long? categoryId, int pageIndex, int pageSize)
        {
            var testContext = await TestingEntities.CreateTestCatalogContext();

            var pictureServiceMock = new Mock<IPictureService>();
            pictureServiceMock.Setup(s => s.FullPathToPicture(It.IsAny<string>())).Returns<string>(value => value);

            var mapperConf = new MapperConfiguration(cfg => cfg.AddProfile(new DefaultAutoMapperProfile()));
            var mapper = new Mapper(mapperConf);

            var categoryRepositoriesMock = new Mock<ICatalogCategoriesRepositoryAsync>();
            var brandRepositoryMock = new Mock<ICatalogBrandRepositoryAsync>();

            var catalogRepositoryMock = new Mock<ICatalogRepositoryAsync>();
            var items = new List<CatalogItem> {
                new CatalogItem {
                    CatalogCategoryId = 1,
                    CatalogBrandId = 1,
                    Code ="ApplesAntonovka",
                    Name="Антоновка",
                    PicturePath = "ApplesAntonovka",
                    Description = "Вкусные  яблоки из антоновки. Описание Описание Описание " +
                        "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                },
                new CatalogItem{
                    CatalogCategoryId= 1,
                    CatalogBrandId = 1,
                    Code="ApplesGreen",
                    Name="Зеленые яблоки",
                    PicturePath = "ApplesGreen",
                    Description = "Вкусные зеленые яблоки просто яблоки. Описание Описание Описание " +
                        "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                }
            }.BuildMock();
            catalogRepositoryMock.Setup(s => s.GetAllItemsQuery()).Returns(items);
            var catalogService = new CatalogService(context: testContext,
                catalogRepository: catalogRepositoryMock.Object,
                categoriesRepository: categoryRepositoriesMock.Object,
                brandRepository: brandRepositoryMock.Object,
                pictureHelper: pictureServiceMock.Object,
                mapper: mapper);
            var filter = new GetItemsFilterDto { CategoryId = categoryId, BrandId = brandId, PageSize = pageSize, PageIndex = pageIndex };


            var result = await catalogService.GetItemsAsync(filter);
            

            Assert.NotNull(result);             
            Assert.Equal(pageSize, result.TotalCount);
        }
    }
}
