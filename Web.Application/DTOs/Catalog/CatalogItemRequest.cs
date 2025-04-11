using System.ComponentModel.DataAnnotations;

namespace Web.Application.DTOs.Catalog
{
    public record class CatalogItemRequest
    {        
        public long Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Code { get; init; }
        [Required]
        public decimal Price { get; init; }
        public string? Description { get; init; }
        [Required]
        public long CatalogBrandId { get; init; }
        [Required]
        public long CatalogCategoryId { get; init; }
        public string PicturePath { get; init; }                
    }
}
