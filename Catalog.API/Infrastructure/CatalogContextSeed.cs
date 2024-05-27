using Catalog.API.Model;
using System.ComponentModel;

namespace Catalog.API.Infrastructure
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext catalogContext)
        {
            if (!catalogContext.CatalogCategories.Any())
            {
                await catalogContext.CatalogCategories.AddRangeAsync(new List<CatalogCategory>
                {
                    new CatalogCategory{Name="Фрукты",Code="Fruits"},
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogBrands.Any())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(new List<CatalogBrand>
                {
                    new CatalogBrand{Code="GreenGarden",Name="Зеленый сад"},
                    new CatalogBrand{Code="Pridonye",Name="Придонье" }
                });
                await catalogContext.SaveChangesAsync();
            }
            if (!catalogContext.CatalogItems.Any())
            {
                var brands = catalogContext.CatalogBrands.ToList();
                var fruitsCategory = await catalogContext.CatalogCategories.FirstOrDefaultAsync(f => f.Code == "Fruits");
                var gardenBrand = await catalogContext.CatalogBrands.FirstOrDefaultAsync(f => f.Code == "GreenGarden");
                var pridonyeBrand = await  catalogContext.CatalogBrands.FirstOrDefaultAsync(f => f.Code == "Pridonye");
                await catalogContext.CatalogItems.AddRangeAsync(new List<CatalogItem>
                {
                    new CatalogItem {
                        CatalogCategoryId=fruitsCategory.Id,
                        CatalogBrandId = gardenBrand.Id,
                        Code ="ApplesAntonovka",
                        Name="Антоновка",
                        PicturePath = "ApplesAntonovka",
                        Description = "Вкусные  яблоки из антоновки. Описание Описание Описание " +
                            "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                    },
                    new CatalogItem{
                        CatalogCategoryId=fruitsCategory.Id,
                        CatalogBrandId = pridonyeBrand.Id,
                        Code="ApplesGreen",
                        Name="Зеленые яблоки",
                        PicturePath = "ApplesGreen",
                        Description = "Вкусные зеленые яблоки просто яблоки. Описание Описание Описание " +
                            "Описание  Описание  Описание  Описание  Описание  Описание  Описание "
                    }
                });
                await catalogContext.SaveChangesAsync();
            }


        }
    }
}
