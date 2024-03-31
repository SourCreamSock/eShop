using Microsoft.AspNetCore.Mvc;
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
            return View(model);
        }
    }
}
