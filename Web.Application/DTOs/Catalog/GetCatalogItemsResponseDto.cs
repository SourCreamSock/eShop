using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Web.Application.DTOs.Catalog
{
    public record class GetCatalogItemsResponseDto
    {
        public IList<CatalogItemResponseDto> CatalogItems { get; init; }
        public long TotalCount { get; init; } 
    }
}
