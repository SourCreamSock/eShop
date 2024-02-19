using Catalog.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    public class CatalogController : ControllerBase
    {
        private CatalogContext _context;
        public CatalogController(CatalogContext context) { 
            _context = context;
        }

        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)//а точно FromQuery
        {
            var items = await _context.CatalogItems.Skip(pageSize*pageIndex).Take(pageSize).ToListAsync();
            return Ok(items);
        }
    }
}
