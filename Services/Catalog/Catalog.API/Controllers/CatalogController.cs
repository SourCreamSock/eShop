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
using static Web.Application.Services.CatalogService;

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
        public async Task<IActionResult> ItemsAsync([FromQuery] GetItemsFilterDto filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            CatalogItemsResponseDto response = await _catalogService.GetItemsAsync(filter);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CatalogItem),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items/{id:long}")]
        public async Task<IActionResult> ItemAsync(long id)//а точно FromQuery
        {

            var item = await _catalogService.GetItemByIdAsync(id);
            if (item != null)                            
                return Ok(item);            
            else
                return NotFound();

        }
        [HttpPost]
        [Route("items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItemAsync([FromBody] CatalogItemCreateRequestDto item)
        {
            var newItem = await _catalogService.AddItem(item);
            var actionName = nameof(ItemAsync);
            return CreatedAtAction(actionName, newItem, null);
        }
        [HttpDelete]
        [Route("items/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItemAsync(long id)
        {
            await _catalogService.DeleteItem(id);
            return Ok();    
        }

        [HttpPut]
        [ProducesResponseType(typeof(CatalogItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items/{id:long}")]
        public async Task<IActionResult> UpdateItemAsync(long id,[FromBody] CatalogItemUpdateRequestDto catalogItemRequest)
        {
            var dbItem = await _catalogService.GetItemByIdAsync(id);
            if (dbItem == null)
                return NotFound();
            await _catalogService.UpdateItem(catalogItemRequest);            
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogCategoryResponseDto>), StatusCodes.Status200OK)]        
        [Route("categories")]
        [SwaggerOperation(Tags = new[] { "Categories"})]
        public async Task<IActionResult> CategoriesAsync()//а точно FromQuery
        {

            var categories = await _catalogService.GetCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogBrandResponseDto>), StatusCodes.Status200OK)]
        [Route("brands")]
        [SwaggerOperation(Tags = new[] { "Brands" })]
        public async Task<IActionResult> BrandsAsync(long? categoryId)//а точно FromQuery
        {

            var brands = _catalogService.GetBrandsAsync(categoryId);
            return Ok(brands);
        }
       
    }
}
