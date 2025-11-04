using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;
using Web.Domain.Repositories;
using Web.Persistence.Catalog;

namespace Web.Application.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogContext _context;
        private readonly IPictureService _pictureHelper;
        private readonly IMapper _mapper;
        private readonly ICatalogRepositoryAsync _catalogRepository;
        private readonly ICatalogCategoriesRepositoryAsync _categoriesRepository;
        private readonly ICatalogBrandRepositoryAsync _brandRepository;
        public CatalogService(CatalogContext context, 
            ICatalogRepositoryAsync catalogRepository,
            ICatalogCategoriesRepositoryAsync categoriesRepository,
            ICatalogBrandRepositoryAsync brandRepository, 
            IPictureService pictureHelper,
            IMapper mapper)
        {
            _context = context;
            _pictureHelper = pictureHelper;
            _mapper = mapper;
            _categoriesRepository = categoriesRepository;
            _catalogRepository = catalogRepository;
            _brandRepository = brandRepository;
        }

        public async Task<GetCatalogItemsResponseDto> GetItemsAsync(GetItemsFilterDto filter)
        {
            var queryItems = _catalogRepository.GetAllItemsQuery();
            if (filter.CategoryId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogCategoryId == filter.CategoryId.Value);
            }
            if (filter.BrandId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogBrandId == filter.BrandId.Value);
            }

            var responseItems = await queryItems
                .Skip(filter.PageSize * filter.PageIndex)
                .Take(filter.PageSize)
                .AsNoTracking()
                .Select(item=>new CatalogItemResponseDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,   
                    Price = item.Price,
                    Description = item.Description,
                    CatalogBrandId = item.CatalogBrandId,
                    CatalogCategoryId = item.CatalogCategoryId,
                    PictureUri = _pictureHelper.FullPathToPicture(item.PicturePath)
                })
                .ToListAsync();
                                 
            GetCatalogItemsResponseDto response = new GetCatalogItemsResponseDto
            {
                CatalogItems = responseItems,
                TotalCount = queryItems.LongCount()
            };
            return response;
        }
        public async Task<CatalogItemResponseDto> GetItemByIdAsync(long id)
        {
            CatalogItemResponseDto responseItem = null;
            var dbItem = await _catalogRepository.GetItemByIdAsync(id);      
            if (dbItem != null)
            {
                responseItem = new CatalogItemResponseDto
                {
                    Id = dbItem.Id,
                    Name = dbItem.Name,
                    Code = dbItem.Code,
                    Price = dbItem.Price,
                    Description = dbItem.Description,
                    CatalogBrandId = dbItem.CatalogBrandId,
                    CatalogCategoryId = dbItem.CatalogCategoryId,
                    PictureUri = _pictureHelper.FullPathToPicture(dbItem.PicturePath)
                };                
            }
                
            return responseItem;
        }
        public async Task<CatalogItem> AddItem(CatalogItemCreateRequestDto dto)
        {            
            var newItem = _mapper.Map<CatalogItem>(dto);
            newItem = await _catalogRepository.AddAsync(newItem);            
            await _context.SaveChangesAsync();
            return newItem;
        }
        public async Task UpdateItem(CatalogItemUpdateRequestDto dto)
        {
            var item = _mapper.Map<CatalogItem>(dto);
            _catalogRepository.Update(item);            
            await _context.SaveChangesAsync();
        }         
        public async Task DeleteItem(long itemId)
        {
            var item = await _catalogRepository.GetItemByIdAsync(itemId);
            _catalogRepository.Delete(item);
            await _context.SaveChangesAsync();            
        }

        public async Task<IList<CatalogCategoryResponseDto>> GetCategoriesAsync()
        {
            var categories =  _categoriesRepository.GetAllCategoriesQueryAsync();
            var mappedCategories = await categories.Select(s => _mapper.Map<CatalogCategoryResponseDto>(s)).ToListAsync();
            return mappedCategories;
        }

        public async Task<IList<CatalogBrandResponseDto>> GetBrandsAsync(long? categoryId)
        {
            var brands = _brandRepository.GetAllBrandsQueryAsync();
            if (categoryId.HasValue)
            {
                var allowedBrandIds = await _catalogRepository.GetAllItemsQuery()
                    .Where(w => w.CatalogCategoryId == categoryId)
                    .Select(f => f.CatalogBrandId)
                    .Distinct()
                    .ToListAsync();
                brands = brands.Where(brand => allowedBrandIds.Contains(brand.Id));
                    
            }
            var mappedBrands = await brands.Select(brand => _mapper.Map<CatalogBrandResponseDto>(brand))
                    .ToListAsync();
            return mappedBrands;
        }
    }
}
