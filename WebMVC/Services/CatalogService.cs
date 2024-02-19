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
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public  async Task<List<CatalogItem>> GetItems(int? page = null, int? pageIndex = null)
        {
            var queryParams = new NameValueCollection();
            var uri = API.Catalog.GetAllItems();
            var uriBuilder =new UriBuilder(uri);                        
            if (page.HasValue)
                queryParams["page"] = page.Value.ToString();
            if (pageIndex.HasValue)
                queryParams["pageIndex"] = pageIndex.Value.ToString();
            uriBuilder.Query = queryParams.ToString();
            var response = await _httpClient.GetAsync(uriBuilder.ToString());
            var items = JsonSerializer.Deserialize<List<CatalogItem>>(response.Content.ReadAsStream());
            return items;
        }
    }
}
