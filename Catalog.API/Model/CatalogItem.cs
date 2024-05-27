using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItem
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public long CatalogBrandId { get; set; }        
        [Required]
        public long CatalogCategoryId { get; set; }
        public string PicturePath { get; set; }
        public CatalogBrand CatalogBrand { get; set; }        
        public CatalogCategory CatalogCategory { get; set; }        
        //public string PictureUri { get; set; }        

    }
}
