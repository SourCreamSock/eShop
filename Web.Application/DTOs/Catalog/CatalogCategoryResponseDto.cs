namespace Web.Application.DTOs.Catalog
{
    public record class CatalogCategoryResponseDto
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Code { get; init; }
    }
}
