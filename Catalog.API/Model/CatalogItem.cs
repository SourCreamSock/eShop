using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }        
        public CatalogItemCategory CatalogItemCategory { get; set; }        
        public string PictureUri { get; set; }        
     
    }
}
