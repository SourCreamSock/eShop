using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public string? Description { get; set; }
        [Required]
        public long CatalogBrandId { get; set; }
        [Required]
        public long CatalogCategoryId { get; set; }
        [JsonIgnore]
        public string PicturePath { get; set; }
        public string? PictureUri { get; set; }
        [JsonIgnore]
        public CatalogBrand CatalogBrand { get; set; }
        [JsonIgnore]
        public CatalogCategory CatalogCategory { get; set; }
        //public string PictureUri { get; set; }        

    }
}
