using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var catalogItems = await _catalogService.GetItems();
            var model = new CatalogViewModel { CatalogItems = catalogItems };
            ViewBag.ItemPageUrl = await _catalogService.ItemPageUrl();
            return View(model);
        }
        public async Task<IActionResult> CatalogItem(long itemId)
        {
            var catalogItemDetailed = await _catalogService.GetItem(itemId);            
            return View(catalogItemDetailed);
        }
    }
}
