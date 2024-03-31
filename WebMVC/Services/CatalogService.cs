using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
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
        public CatalogService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _urlCatalog = configuration.GetValue<string>("catalog-api-url");
        }
        /// <summary>
        /// Получить объекты 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CatalogItem>> GetItems(int? pageSize = null, int? pageIndex = null)
        {
            try
            {
                var queryParams = new NameValueCollection();
                var urlItems = API.Catalog.GetAllItems();
                var uriBuilder = new UriBuilder(_urlCatalog + urlItems);
                if (pageSize.HasValue)
                    queryParams["pageSize"] = pageSize.Value.ToString();
                if (pageIndex.HasValue)
                    queryParams["pageIndex"] = pageIndex.Value.ToString();
                uriBuilder.Query = queryParams.ToString();
                var response = await _httpClient.GetAsync(uriBuilder.ToString());
                var items = JsonSerializer.Deserialize<IEnumerable<CatalogItem>>(response.Content.ReadAsStream());
                return items;
            }
            catch (Exception)
            {
                return new List<CatalogItem> { };
                throw;
            }
        }
    }
}
