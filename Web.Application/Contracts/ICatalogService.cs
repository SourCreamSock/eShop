using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;
using static Web.Application.Services.CatalogService;

namespace Web.Application.Contracts
{
    public interface ICatalogService
    {
        Task<IList<CatalogItem>> GetItemsAsync(GetItemsFilter filter);

    }
}
