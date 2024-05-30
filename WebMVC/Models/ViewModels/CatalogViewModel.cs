using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebMVC.Models.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }           
        public IEnumerable<SelectListItem> CatalogCategories { get; set; }           
        public IEnumerable<SelectListItem> CatalogBrands { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }

    }
}
