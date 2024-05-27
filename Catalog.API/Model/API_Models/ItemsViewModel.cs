using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Model.API_Models
{
    public class ItemsViewModel
    {
        public long Id { get; set; }        
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string PicturePath { get; set; }
    }
}
