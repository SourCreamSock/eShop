using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.DTOs.Catalog
{
    public class CatalogItemUpdateRequestDto
    {
        public long Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Code { get; init; }        
        public decimal Price { get; init; }
        public string? Description { get; init; }        
        public long CatalogBrandId { get; init; }        
        public long CatalogCategoryId { get; init; }
        public string PicturePath { get; init; }
    }
}
