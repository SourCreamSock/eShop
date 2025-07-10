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
    public class CatalogCategoriesRepository : ICatalogCategoriesRepositoryAsync
    {
        private CatalogContext _dbContext;
        public CatalogCategoriesRepository(CatalogContext catalogContext)
        {
            _dbContext = catalogContext;
        }
        public IQueryable<CatalogCategory> GetAllCategoriesQueryAsync()
        {
            return _dbContext.CatalogCategories.AsQueryable();
        }
        
    }
}
