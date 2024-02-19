using Microsoft.AspNetCore.Mvc;
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
            var catalog = _catalogService.GetItems();
        }
    }
}
