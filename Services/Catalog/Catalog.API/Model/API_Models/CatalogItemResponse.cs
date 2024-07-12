using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model.API_Models
{
    public class CatalogItemResponse
    {
        public long Id { get; set; }        
        public string Name { get; set; }        
        public string Code { get; set; }        
        public decimal Price { get; set; }
        public string? Description { get; set; }        
        public long CatalogBrandId { get; set; }        
        public long CatalogCategoryId { get; set; }
        public string PicturePath { get; set; }
        public string? PictureUri { get; set; }
    }
}
