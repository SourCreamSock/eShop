using Catalog.API.Infrastructure;
using Catalog.API.Model.API_Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalog.API.Controllers
{
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IPictureHelper _pictureHelper;
        public CatalogController(CatalogContext context, IPictureHelper pictureHelper) { 
            _context = context;
            _pictureHelper = pictureHelper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ItemsViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] long? categoryId, [FromQuery] long? brandId, 
            [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var queryItems =  _context.CatalogItems.Skip(pageSize * pageIndex).Take(pageSize);
            if (categoryId.HasValue)
            {
                queryItems =  queryItems.Where(w => w.CatalogCategoryId == categoryId.Value);
            }
            if (brandId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogBrandId == brandId.Value);
            }
            var items = await queryItems.ToListAsync();
            items.ForEach(item => item.PictureUri = _pictureHelper.FullPathToPicture(item.PicturePath));

            ItemsViewModel response = new ItemsViewModel
            {
                CatalogItems = items,
                TotalCount = await _context.CatalogItems.LongCountAsync()
            };
            
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CatalogItem),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items/{id:long}")]
        public async Task<IActionResult> ItemAsync(long id)//а точно FromQuery
        {

            var item = await _context.CatalogItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                item.PicturePath = _pictureHelper.FullPathToPicture(item.PicturePath);
                return Ok(item);
            }
            else
                return NotFound();

        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogCategory>), StatusCodes.Status200OK)]        
        [Route("categories")]
        public async Task<IActionResult> CategoriesAsync()//а точно FromQuery
        {

            var categories = await _context.CatalogCategories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogBrand>), StatusCodes.Status200OK)]
        [Route("brands")]
        public async Task<IActionResult> BrandsAsync(long? categoryId)//а точно FromQuery
        {

            var brands = await _context.CatalogBrands.ToListAsync();
            if (categoryId.HasValue)
            {
                var allowedBrandIds = await _context.CatalogItems.Where(w => w.CatalogCategoryId == categoryId).Select(f => f.CatalogBrandId).Distinct().ToListAsync();
                brands = brands.Where(brand => allowedBrandIds.Contains(brand.Id)).ToList();
            }
            return Ok(brands);
        }
    }
}
