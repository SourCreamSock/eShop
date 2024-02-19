namespace WebMVC.Models
{
    public class CatalogItem
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price{ get; set; }
        public int CatalogTypeId { get; init; }
        public string CatalogType { get; init; }
        
    }
}
