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
    public class CatalogBrandRepository : ICatalogBrandRepositoryAsync
    {
        private CatalogContext _dbContext;
        public CatalogBrandRepository(CatalogContext catalogContext)
        {
            _dbContext = catalogContext;
        }
        public IQueryable<CatalogBrand> GetAllBrandsQueryAsync()
        {
            return _dbContext.CatalogBrands.AsQueryable();
        }
    }
}
