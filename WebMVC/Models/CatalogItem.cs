using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Code { get; set; }        
        public decimal Price { get; set; }        
        public int CategoryId { get; set; }        

    }
}
