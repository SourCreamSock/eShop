using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Domain.Entities.Catalog
{
    public class CatalogItem
    {        
        public long Id { get; set; }        
        public string Name { get; set; }        
        public string Code { get; set; }        
        public decimal Price { get; set; }
        public string? Description { get; set; }        
        public long CatalogBrandId { get; set; }        
        public long CatalogCategoryId { get; set; }        
        public string PicturePath { get; set; }                    
        public CatalogBrand CatalogBrand { get; set; }        
        public CatalogCategory CatalogCategory { get; set; }
        //public string PictureUri { get; set; }        

    }
}
