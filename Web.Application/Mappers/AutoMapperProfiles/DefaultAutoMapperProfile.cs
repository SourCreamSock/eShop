using AutoMapper;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;


namespace Web.Application.Mappers.AutoMapperProfiles
{
    public class DefaultAutoMapperProfile : Profile
    {
        public DefaultAutoMapperProfile()
        {
            this.CreateMap<CatalogItem, CatalogItemResponseDto>().ReverseMap();
        }
    }
}
