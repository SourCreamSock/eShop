using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItemAttributeCategory
    {
        public int Id { get; set; }
        [Required]
        public int CatalogItemAttributeId { get; set; }
        public CatalogItemAttribute CatalogItemAttribute { get; set; }
        [Required]
        public int CatalogItemCategoryId { get; set; }
        public CatalogItemCategory CatalogItemCategory { get; set; }
    }
}
