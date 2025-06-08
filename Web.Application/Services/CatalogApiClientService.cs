using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;

namespace Web.Application.Services
{
    public class CatalogApiClientService : ICatalogApiClientService
    {
        //private readonly string _GetAllCatalogItems;
        private readonly HttpClient _httpClient;
        private readonly string _urlCatalog;
        private readonly string _selfUrl;
        public CatalogApiClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlCatalog = configuration.GetValue<string>("CatalogApiUrl") ?? throw new ArgumentNullException();
            _selfUrl = configuration.GetValue<string>("SelfUrl") ?? throw new ArgumentNullException();
        }

        public async Task<CatalogItemsResponseDto> GetItems(long? categoryId, long? brandId, int? pageIndex, int? pageSize = null)
        {

            var queryParams = new Dictionary<string, string>();
            var urlItems = "items";
            var uriBuilder = new UriBuilder(_urlCatalog + urlItems);
            if (categoryId.HasValue)
                queryParams["categoryId"] = categoryId.Value.ToString();
            if (brandId.HasValue)
                queryParams["brandId"] = brandId.Value.ToString();
            if (pageIndex.HasValue)
                queryParams["pageIndex"] = pageIndex.Value.ToString();
            if (pageSize.HasValue)
                queryParams["pageSize"] = pageSize.Value.ToString();
            uriBuilder.Query = string.Join('&', queryParams.Select(s => s.Key + "=" + s.Value));
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var result = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<CatalogItemsResponseDto>(result);
            return items;

        }
        public async Task<CatalogItemResponseDto> GetItem(long itemId)
        {
            try
            {
                var queryParams = new Dictionary<string, string>();
                var url = _urlCatalog + $"items/{itemId}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<CatalogItemResponseDto>(result);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<CatalogCategoryResponseDto>> GetCategories()
        {
            try
            {
                var url = _urlCatalog + $"categories";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<IEnumerable<CatalogCategoryResponseDto>>(result);
                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<CatalogBrandResponseDto>> GetBrands(long? categoryId = null)
        {
            try
            {
                var url = _urlCatalog + $"brands";
                if (categoryId.HasValue)
                    url += $"?categoryId={categoryId}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<IEnumerable<CatalogBrandResponseDto>>(result);
                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> ItemPageUrl()
        {
            return _selfUrl + "Catalog/CatalogItem/";
        }
    }
}
