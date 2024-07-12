using AutoMapper;
using Catalog.API.Model.API_Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catalog.API.Infrastructure.AutoMapperProfiles
{
    public class DefaultAutoMapperProfile : Profile
    {
        public DefaultAutoMapperProfile()
        {
            this.CreateMap<CatalogItem, CatalogItemResponse>().ReverseMap();
        }
    }
}
