namespace WebMVC.Models
{
    public class CatalogItemsResponse
    {
        public List<CatalogItem> CatalogItems  { get; set; }
        public long TotalCount { get; set; }
    }
}
