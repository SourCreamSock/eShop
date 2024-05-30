using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;
using WebMVC.Models.ViewModels;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class CatalogController: Controller
    {
        private CatalogService _catalogService;
        public CatalogController(CatalogService catalogService ) { 
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(long? categoryId = null, long? brandId = null)
        {
            var catalogItems = await _catalogService.GetItems();
            var catalogCategories = await _catalogService.GetCategories();
            var catalogBrands = await _catalogService.GetBrands();
            var model = new CatalogViewModel { CatalogItems = catalogItems,
                CatalogBrands = catalogBrands.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }),
                CatalogCategories = catalogCategories.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }),
                CategoryId = categoryId,
                BrandId = brandId
            };            
            ViewBag.ItemPageUrl = await _catalogService.ItemPageUrl();
            return View(model);
        }
        public async Task<IActionResult> CatalogItem(long itemId)
        {
            var catalogItemDetailed = await _catalogService.GetItem(itemId); 
            return View(catalogItemDetailed);
        }
        [HttpGet]
        public async Task<IEnumerable<CatalogCategory>> GetCatalogCategories()
        {
            var items = await _catalogService.GetCategories();
            return items;
        }
        [HttpGet]
        public async Task<IEnumerable<CatalogBrand>> GetCatalogBrands(long? categoryId)
        {
            var items = await _catalogService.GetBrands(categoryId);
            return items;
        }
    }
}
