using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities.Catalog;

namespace Web.Domain.Repositories
{
    public interface ICatalogRepositoryAsync
    {
        Task<CatalogItem?> GetItemByIdAsync(long id);
        IQueryable<CatalogItem> GetAllItemsQuery();

        Task<IList<CatalogItem>> GetAllItemsAsync();

        Task<CatalogItem> AddAsync(CatalogItem item);

        void Update(CatalogItem item);

        void Delete(CatalogItem item);

        void DeleteRange(IList<CatalogItem> items);
        
    }
}
