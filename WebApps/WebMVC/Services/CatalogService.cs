using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CatalogService
    {
        //private readonly string _GetAllCatalogItems;
        private readonly HttpClient _httpClient;
        private readonly string _urlCatalog;
        private readonly string _selfUrl;
        public CatalogService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlCatalog = configuration.GetValue<string>("CatalogApiUrl");
            _selfUrl = configuration.GetValue<string>("SelfUrl");
        }
      
        public async Task<CatalogItemsResponse> GetItems(long? categoryId, long? brandId, int? pageIndex, int? pageSize = null )
        {
            try
            {
                var queryParams = new Dictionary<string,string>();
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
                var items = JsonConvert.DeserializeObject<CatalogItemsResponse>(result);
                return items;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public async Task<CatalogItemDetailed> GetItem(long itemId)
        {
            try
            {
                var queryParams = new Dictionary<string, string>();
                var url = _urlCatalog + $"items/{itemId}";                
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<CatalogItemDetailed>(result);
                return item;
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
        public async Task<IEnumerable<CatalogCategory>> GetCategories()
        {
            try
            {                
                var url = _urlCatalog + $"categories";                
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<IEnumerable<CatalogCategory>>(result);
                return items;
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
        public async Task<IEnumerable<CatalogBrand>> GetBrands(long? categoryId = null)
        {
            try
            {
                var url = _urlCatalog + $"brands";
                if (categoryId.HasValue)
                    url += $"?categoryId={categoryId}";
                var response = await _httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<IEnumerable<CatalogBrand>>(result);
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
