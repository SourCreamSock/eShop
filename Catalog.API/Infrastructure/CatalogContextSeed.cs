namespace Catalog.API.Infrastructure
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogContext catalogContext)
        {
            catalogContext.CatalogTypes.AddRange(new List<CatalogType>
            {
                new CatalogType{Name="Fruits"}                
            });
            await catalogContext.SaveChangesAsync();

            catalogContext.CatalogItems.AddRange(new List<CatalogItem>
            {
                new CatalogItem{Name="Oranges",Description="Tasty Oranges from SomeWhere"},
                new CatalogItem{Name="Apples",Description="Red Apples"}
            });
            await catalogContext.SaveChangesAsync();
        }
    }
}
