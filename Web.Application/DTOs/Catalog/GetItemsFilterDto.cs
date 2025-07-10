using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.DTOs.Catalog
{
    public class GetItemsFilterDto
    {
        public GetItemsFilterDto(long? categoryId, long? brandId, int pageSize, int pageIndex)
        {
            CategoryId = categoryId;
            BrandId = brandId;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        [Range(1, long.MaxValue, ErrorMessage = "CategoryId дожен быть больше 1.")]
        public long? CategoryId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "BrandId должен быть больше 1.")]
        public long? BrandId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageSize должен быть больше 1.")]
        public int PageSize { get; set; } = 10;

        [Range(0, int.MaxValue, ErrorMessage = "PageIndex должен быть больше 0.")]
        public int PageIndex { get; set; } = 0;
    }
}
