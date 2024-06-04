using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Catalog.API.Model.API_Models
{
    public class ItemsViewModel
    {
        public List<CatalogItem> CatalogItems { get; set; }
        public long TotalCount { get; set; } 
    }
}
