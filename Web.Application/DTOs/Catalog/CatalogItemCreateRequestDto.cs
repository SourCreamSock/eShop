using System.ComponentModel.DataAnnotations;

namespace Web.Application.DTOs.Catalog
{
    public record class CatalogItemCreateRequestDto
    {
        public long Id { get; init; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; init; }
        [Required(AllowEmptyStrings = false)]
        public string Code { get; init; }
        [Required]
        public decimal Price { get; init; }
        public string? Description { get; init; }
        [Required, Range(1, long.MaxValue, ErrorMessage = "CatalogBrandId должен быть больше 1.")]
        public long CatalogBrandId { get; init; }
        [Required, Range(1, long.MaxValue, ErrorMessage = "CatalogCategoryId должен быть больше 1.")]
        public long CatalogCategoryId { get; init; }
        public string PicturePath { get; init; }
    }
}
