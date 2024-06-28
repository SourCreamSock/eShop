using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model.API_Models
{
    public record class CatalogItemRequest
    {        
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public long CatalogBrandId { get; set; }
        [Required]
        public long CatalogCategoryId { get; set; }
        public string PicturePath { get; set; }                
    }
}
