using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Web.Application.DTOs.Catalog
{
    public record class CatalogItemsResponseDto
    {
        public List<CatalogItemResponseDto> CatalogItems { get; init; }
        public long TotalCount { get; init; } 
    }
}
