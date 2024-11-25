﻿using AutoMapper;
using Catalog.API.Infrastructure;
using Catalog.API.Model.API_Models;
using Catalog.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Catalog.API.Controllers
{
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IPictureHelper _pictureHelper;
        private readonly IMapper _mapper;
        public CatalogController(CatalogContext context, IPictureHelper pictureHelper, IMapper mapper) {
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
        [ProducesResponseType(typeof(CatalogItemsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] ItemFilter filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var queryItems = _context.CatalogItems.AsQueryable();
            if (filter.CategoryId.HasValue)
            {
                queryItems =  queryItems.Where(w => w.CatalogCategoryId == filter.CategoryId.Value);
            }
            if (filter.BrandId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogBrandId == filter.BrandId.Value);
            }
            
            var items = await queryItems.Skip(filter.PageSize * filter.PageIndex).Take(filter.PageSize).Select(item=>_mapper.Map<CatalogItemResponse>(item)).ToListAsync();
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
        [Route("items/{id:long}")]
        public async Task<IActionResult> UpdateItemAsync(long id,[FromBody] CatalogItemRequest catalogItemRequest)
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
        [ProducesResponseType(typeof(List<CatalogCategory>), StatusCodes.Status200OK)]        
        [Route("categories")]
        [SwaggerOperation(Tags = new[] { "Categories"})]
        public async Task<IActionResult> CategoriesAsync()//а точно FromQuery
        {

            var categories = await _context.CatalogCategories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<CatalogBrand>), StatusCodes.Status200OK)]
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
        public class ItemFilter
        {
            public ItemFilter(long? categoryId, long? brandId, int pageSize, int pageIndex)
            {
                CategoryId = categoryId;
                BrandId = brandId;
                PageSize = pageSize;
                PageIndex = pageIndex;
            }

            [Range(1, long.MaxValue, ErrorMessage = "Category ID must be greater than or equal to 1.")]
            public long? CategoryId { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Brand ID must be greater than or equal to 1.")]
            public long? BrandId { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than or equal to 1.")]
            public int PageSize { get; set; } = 10;

            [Range(0, int.MaxValue, ErrorMessage = "Page index must be greater than or equal to 0.")]
            public int PageIndex { get; set; } = 0;
        }
    }
}
