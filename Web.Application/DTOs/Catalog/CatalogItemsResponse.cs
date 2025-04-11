using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Web.Application.DTOs.Catalog
{
    public record class CatalogItemsResponse
    {
        public List<CatalogItemResponse> CatalogItems { get; init; }
        public long TotalCount { get; init; } 
    }
}
