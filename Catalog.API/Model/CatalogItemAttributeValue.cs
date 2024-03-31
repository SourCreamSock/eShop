using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItemAttributeValue
    {
        public int Id { get; set; }
        public string? ValueText { get; set; }
        public long? ValueNumber { get; set; }
        public bool? ValueBool { get; set; }
        public double? ValueFloat { get; set; }
        public DateTime? ValueDate { get; set; }
        [Required]
        public int CatalogItemAttributeId { get; set; }
        public CatalogItemAttribute CatalogItemAttribute { get; set; }
        [Required]
        public int CatalogItemId { get; set; }
        public CatalogItem CatalogItem { get; set; }
        
    }
}
