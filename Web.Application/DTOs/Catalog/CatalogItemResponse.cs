using System.ComponentModel.DataAnnotations;

namespace Web.Application.DTOs.Catalog
{
    public record class CatalogItemResponse
    {
        public long Id { get; init; }        
        public string Name { get; init; }        
        public string Code { get; init; }        
        public decimal Price { get; init; }
        public string? Description { get; init; }        
        public long CatalogBrandId { get; init; }        
        public long CatalogCategoryId { get; init; }
        public string PicturePath { get; init; }
        public string? PictureUri { get; init; }
    }
}
