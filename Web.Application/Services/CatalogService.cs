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
        public CatalogService(CatalogContext context, ICatalogRepositoryAsync catalogRepository, IPictureService pictureHelper, IMapper mapper)
        {
            _context = context;
            _pictureHelper = pictureHelper;
            _mapper = mapper;
        }

        public async Task<IList<CatalogItem>> GetItemsAsync(GetItemsFilter filter)
        {
            var queryItems = _catalogRepository.GetAllItemsQueryAsync();
            if (filter.CategoryId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogCategoryId == filter.CategoryId.Value);
            }
            if (filter.BrandId.HasValue)
            {
                queryItems = queryItems.Where(w => w.CatalogBrandId == filter.BrandId.Value);
            }

            var dbItems = await queryItems
                .Skip(filter.PageSize * filter.PageIndex)
                .Take(filter.PageSize)
                .AsNoTracking()
                .ToListAsync();
            dbItems.ForEach(item => item.PicturePath = _pictureHelper.FullPathToPicture(item.PicturePath));

            var responseItems = dbItems.ToList();
            return responseItems;
        }
        public class GetItemsFilter
        {
            public long? CategoryId { get; set; }
            public long? BrandId { get; set; }            
            public int PageSize { get; set; }             
            public int PageIndex { get; set; }
        }
    }
}
