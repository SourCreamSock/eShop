using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities.Catalog;

namespace Web.Domain.Repositories
{
    public interface ICatalogBrandRepositoryAsync
    {
        IQueryable<CatalogBrand> GetAllBrandsQueryAsync();

        //Task<IList<CatalogItem>> GetAllItemsAsync();
     
    }
}
