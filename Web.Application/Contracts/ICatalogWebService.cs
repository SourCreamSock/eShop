using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.Catalog;

namespace Web.Application.Contracts
{
    public interface ICatalogWebService
    {
        public Task<CatalogItemsResponseDto> GetItems(long? categoryId, long? brandId, int? pageIndex, int? pageSize);
        public Task<CatalogItemResponseDto> GetItem(long itemId);
        public Task<IEnumerable<CatalogCategoryResponseDto>> GetCategories();
        public Task<IEnumerable<CatalogBrandResponseDto>> GetBrands(long? categoryId);
        public Task<string> ItemPageUrl();
    }
}
