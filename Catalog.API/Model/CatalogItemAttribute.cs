using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model
{
    public class CatalogItemAttribute
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Type { get; set; }
    }
}
