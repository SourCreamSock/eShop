using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Application.DTOs.Catalog;
using Web.Application.Services;
using WebMVC.Models;
using WebMVC.Models.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController: Controller
    {
        private CatalogWebService _catalogService;
        public CatalogController(CatalogWebService catalogService ) { 
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(long? categoryId, long? brandId, int? pageIndex)
        {
            int pageSize = 9;
            var response = await _catalogService.GetItems(categoryId, brandId, pageIndex, pageSize: pageSize);
            var catalogCategories = await _catalogService.GetCategories();
            var catalogBrands = await _catalogService.GetBrands();
            var model = new CatalogViewModel {
                CatalogItems = response.CatalogItems,
                CatalogBrands = catalogBrands.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }),
                CatalogCategories = catalogCategories.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }),
                CategoryId = categoryId,
                BrandId = brandId,
                PageIndex = pageIndex ?? 0,
                PageCount = (int)Math.Ceiling((decimal)response.TotalCount / pageSize)
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
        public async Task<IEnumerable<CatalogCategoryResponseDto>> GetCatalogCategories()
        {
            var items = await _catalogService.GetCategories();
            return items;
        }
        [HttpGet]
        public async Task<IEnumerable<CatalogBrandResponseDto>> GetCatalogBrands(long? categoryId)
        {
            var items = await _catalogService.GetBrands(categoryId);
            return items;
        }
    }
}
