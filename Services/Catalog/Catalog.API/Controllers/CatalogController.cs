 using AutoMapper;
using Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;
using Web.Persistence.Catalog;

namespace Catalog.API.Controllers
{
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        public CatalogController(ICatalogService catalogService, IMapper mapper)
        {
            _catalogService = catalogService;
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
        [ProducesResponseType(typeof(CatalogItemsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] GetItemsFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mappedFilter = _mapper.Map<Web.Application.Services.CatalogService.GetItemsFilter>(filter);
            var catalogItems = await _catalogService.GetItemsAsync(mappedFilter);
            var catalogDtoItems = _mapper.Map<IList<CatalogItemResponseDto>>(catalogItems);
            CatalogItemsResponseDto response = new CatalogItemsResponseDto
            {
                CatalogItems = catalogDtoItems,
                TotalCount =  catalogDtoItems.Count()
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
        [Route("items/{id:long}")]
        public async Task<IActionResult> UpdateItemAsync(long id,[FromBody] CatalogItemRequestDto catalogItemRequest)
        {
            var isItemExist = await _context.CatalogItems.AnyAsync(s => s.Id == id);
            if (!isItemExist)
                return NotFound();
            var dbCatalogItem = _mapper.Map<CatalogItem>(catalogItemRequest);            
            _context.CatalogItems.Update(dbCatalogItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogCategoryResponseDto>), StatusCodes.Status200OK)]        
        [Route("categories")]
        [SwaggerOperation(Tags = new[] { "Categories"})]
        public async Task<IActionResult> CategoriesAsync()//а точно FromQuery
        {

            var categories = await _context.CatalogCategories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogBrandResponseDto>), StatusCodes.Status200OK)]
        [Route("brands")]
        [SwaggerOperation(Tags = new[] { "Brands" })]
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
