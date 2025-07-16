using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entities.Catalog;
using Web.Domain.Repositories;
using Web.Persistence.Catalog;

namespace Web.Application.Repositories
{
    public class CatalogRepositoryAsync : ICatalogRepositoryAsync
    {
        private CatalogContext _dbContext;
        public CatalogRepositoryAsync(CatalogContext catalogContext)
        {
            _dbContext = catalogContext;
        }
        public async Task<CatalogItem?> GetItemByIdAsync(long id)
        {
            return await _dbContext.CatalogItems.FindAsync(id);
        }
        public IQueryable<CatalogItem> GetAllItemsQuery()
        {
            return _dbContext.CatalogItems.AsQueryable();
        }
        public async Task<IList<CatalogItem>> GetAllItemsAsync()
        {
            return await _dbContext.CatalogItems.ToListAsync();
        }
        public async Task<CatalogItem> AddAsync(CatalogItem item)
        {
            await _dbContext.CatalogItems.AddAsync(item);
            return item;
        }
        public void Update(CatalogItem item)
        {
            _dbContext.Update(item);
        }
        public void Delete(CatalogItem item)
        {
            _dbContext.Remove(item);
        }
        public void DeleteRange(IList<CatalogItem> items)
        {
            _dbContext.RemoveRange(items);
        }
    }
}
