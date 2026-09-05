 using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Utils.Extensions;
using Web.Domain.Entities.Catalog;
using Web.Persistence.Catalog;
using static Web.Application.Services.CatalogService;

namespace Catalog.API.Controllers
{
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetItemsFilterDto> _validator;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogService catalogService, 
            IMapper mapper, 
            IValidator<GetItemsFilterDto> validator,
            ILogger<CatalogController> logger)
        {
            _catalogService = catalogService;
            _mapper = mapper;          
            _validator = validator;
            _logger = logger;
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
        [ProducesResponseType(typeof(GetCatalogItemsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] GetItemsFilterDto filter)
        {
            var validatorResult = await _validator.ValidateAsync(filter);
            if (!validatorResult.IsValid)
            {
                validatorResult.AddToMVCModelState(ModelState);
                return BadRequest(ModelState);
            }
            
            GetCatalogItemsResponseDto response = await _catalogService.GetItemsAsync(filter);

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
            try
            {
                _logger.LogInformation($"CreateItemAsync request {@item}", item);
                var newItem = await _catalogService.AddItem(item);
                var actionName = nameof(ItemAsync);
                return CreatedAtAction(actionName, newItem, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on CreateItemAsync request {@item}", item);
                throw;
            }

        }
        [HttpDelete]
        [Route("items/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItemAsync(long id)
        {
            try
            {
                _logger.LogInformation($"DeleteItemAsync request {@id}", id);
                await _catalogService.DeleteItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on DeleteItemAsync request {@id}", id);
                throw;
            } 
        }

        [HttpPut]
        [ProducesResponseType(typeof(CatalogItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items/{id:long}")]
        public async Task<IActionResult> UpdateItemAsync(long id,[FromBody] CatalogItemUpdateRequestDto catalogItemRequest)
        {
            try
            {
                _logger.LogInformation("UpdateItemAsync request id:{id}, body: {@catalogItemRequest}", id, catalogItemRequest);
                var dbItem = await _catalogService.GetItemByIdAsync(id);
                if (dbItem == null)
                    return NotFound();
                await _catalogService.UpdateItem(catalogItemRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error on DeleteItemAsync request id:{id}, body: {@catalogItemRequest}", id, catalogItemRequest);
                throw;
            }
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

            var brands = await _catalogService.GetBrandsAsync(categoryId);
            return Ok(brands);
        }
       
    }
}
