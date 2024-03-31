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
            if (!catalogContext.CatalogItems.Any()) {
                await catalogContext.CatalogItems.AddRangeAsync(new List<CatalogItem>
                {
                    new CatalogItem{Code="Oranges",Name="Опельсины из Зимбабуве",Price=3,CategoryId=1},
                    new CatalogItem{Code="ApplesAntonovka",Name="Антоновка",Price= 11,CategoryId=1},
                    new CatalogItem{Code="ApplesGreen",Name="Зеленые яблоки",Price= 12,CategoryId=1}
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
                    new CatalogItemAttributeCategory{CatalogItemAttributeId=1,CatalogItemCategoryId=1},
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItemAttributeValues.Any())
            {
                await catalogContext.CatalogItemAttributeValues.AddRangeAsync(new List<CatalogItemAttributeValue>
                {
                    new CatalogItemAttributeValue{CatalogItemId=1,CatalogItemAttributeId =1, ValueText="Московские"},
                    new CatalogItemAttributeValue{CatalogItemId=2,CatalogItemAttributeId =1, ValueText="Придонье"},
                    new CatalogItemAttributeValue{CatalogItemId=3,CatalogItemAttributeId =1, ValueText="Московские"},
                });
                await catalogContext.SaveChangesAsync();
            }



        }
    }
}
