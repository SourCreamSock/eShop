using AutoMapper;
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
        private readonly Mapper _mapper;
        public CatalogController(CatalogContext context, IPictureHelper pictureHelper, Mapper mapper) { 
            _context = context;
            _pictureHelper = pictureHelper;
            _mapper = mapper;
        }
        /// <summary>
        /// Получить товары
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="brandId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CatalogItemsResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] long? categoryId, [FromQuery] long? brandId, 
            [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var queryItems = _context.CatalogItems.AsQueryable();
            if (categoryId.HasValue)
            {
                queryItems =  queryItems.Where(w => w.CatalogCategoryId == categoryId.Value);
            }
            if (brandId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogBrandId == brandId.Value);
            }
            
            var items = await queryItems.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
            items.ForEach(item => item.PictureUri = _pictureHelper.FullPathToPicture(item.PicturePath));

            CatalogItemsResponse response = new CatalogItemsResponse
            {
                CatalogItems = items,
                TotalCount = await queryItems.LongCountAsync()
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

            var item = await _context.CatalogItems.SingleOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                item.PicturePath = _pictureHelper.FullPathToPicture(item.PicturePath);
                return Ok(item);
            }
            else
                return NotFound();

        }
        [HttpPost]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItemAsync([FromBody] CatalogItem item)
        {
            _context.CatalogItems.Add(item);    
            await _context.SaveChangesAsync();
            var actionName = nameof(ItemAsync);
            return CreatedAtAction(actionName, item, null);
        }
        [HttpDelete]
        [Route("items/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItemAsync(long id)
        {
            var item = await _context.CatalogItems.SingleOrDefaultAsync(s => s.Id == id);
            if(item == null)
                return NotFound();
            _context.CatalogItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();    
        }

        [HttpPut]
        [ProducesResponseType(typeof(CatalogItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> UpdateItemAsync([FromBody] CatalogItemRequest catalogItemRequest)
        {
            var isItemExist = await _context.CatalogItems.AnyAsync(s => s.Id == catalogItemRequest.Id);
            if (!isItemExist)
                return NotFound();
            var dbCatalogItem = _mapper.Map<CatalogItem>(catalogItemRequest);
            _context.CatalogItems.Update(dbCatalogItem);
            await _context.SaveChangesAsync();
            return Ok();
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
