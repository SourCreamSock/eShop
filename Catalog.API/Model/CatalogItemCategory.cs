using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItemCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }        
    }
}
