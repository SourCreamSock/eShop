using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;
using static Web.Application.Services.CatalogService;

namespace Web.Application.Contracts
{
    public interface ICatalogService
    {
        Task<GetCatalogItemsResponseDto> GetItemsAsync(GetItemsFilterDto filter);
        Task<CatalogItem> GetItemByIdAsync(long id);
        Task<CatalogItem> AddItem(CatalogItemCreateRequestDto item);
        Task UpdateItem(CatalogItemUpdateRequestDto dto);
        Task DeleteItem(long itemId);
        Task<IList<CatalogCategoryResponseDto>> GetCategoriesAsync();
        Task<IList<CatalogBrandResponseDto>> GetBrandsAsync(long? categoryId);

    }
}
