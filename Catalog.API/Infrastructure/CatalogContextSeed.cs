using Catalog.API.Model;
using System.ComponentModel;

namespace Catalog.API.Infrastructure
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext catalogContext)
        {
            if (!catalogContext.CatalogItemCategories.Any())
            {
                await catalogContext.CatalogItemCategories.AddRangeAsync(new List<CatalogItemCategory>
                {
                    new CatalogItemCategory{Name="Фрукты",Code="Fruits"},
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItems.Any())
            {
                var fruitsCategoryId = catalogContext.CatalogItemCategories.FirstOrDefault(f=>f.Code=="Fruits").Id;
                await catalogContext.CatalogItems.AddRangeAsync(new List<CatalogItem>
                {
                    new CatalogItem{Code="Oranges",Name="Опельсины из Зимбабуве",Price=3,CategoryId=fruitsCategoryId },
                    new CatalogItem{Code="ApplesAntonovka",Name="Антоновка",Price= 11,CategoryId=fruitsCategoryId},
                    new CatalogItem{Code="ApplesGreen",Name="Зеленые яблоки",Price= 12,CategoryId= fruitsCategoryId}
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItemAttributes.Any())
            {
                await catalogContext.CatalogItemAttributes.AddRangeAsync(new List<CatalogItemAttribute>
                {
                    new CatalogItemAttribute{Name="Бренд",Code="Brand",Type="string"},
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItemAttributeCategories.Any())
            {
                await catalogContext.CatalogItemAttributeCategories.AddRangeAsync(new List<CatalogItemAttributeCategory>
                {
                    new CatalogItemAttributeCategory{
                        CatalogItemAttributeId=catalogContext.CatalogItemAttributes.FirstOrDefault(f=>f.Code=="Brand").Id,
                        CatalogItemCategoryId=catalogContext.CatalogItemCategories.FirstOrDefault(f=>f.Code == "Fruits").Id,
                    },
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItemAttributeValues.Any())
            {
                var items = catalogContext.CatalogItems.ToArray();
                var brandAttributeId = catalogContext.CatalogItemAttributes.FirstOrDefault(f=>f.Code=="Brand").Id;
                await catalogContext.CatalogItemAttributeValues.AddRangeAsync(new List<CatalogItemAttributeValue>
                {
                    new CatalogItemAttributeValue{CatalogItemId=items[0].Id,CatalogItemAttributeId =brandAttributeId, ValueText="Московские"},
                    new CatalogItemAttributeValue{CatalogItemId=items[1].Id,CatalogItemAttributeId =brandAttributeId, ValueText="Придонье"},
                    new CatalogItemAttributeValue{CatalogItemId=items[2].Id,CatalogItemAttributeId =brandAttributeId, ValueText="Откуда Нибудь"},
                });
                await catalogContext.SaveChangesAsync();
            }
        }
    }
}
