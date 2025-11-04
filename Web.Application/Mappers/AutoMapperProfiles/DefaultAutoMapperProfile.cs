using AutoMapper;
using Web.Application.DTOs.Catalog;
using Web.Domain.Entities.Catalog;


namespace Web.Application.Mappers.AutoMapperProfiles
{
    public class DefaultAutoMapperProfile : Profile
    {
        public DefaultAutoMapperProfile()
        {
            //this.CreateMap<CatalogItem, CatalogItemResponseDto>().ReverseMap();            
            this.CreateMap<CatalogItem, CatalogItemCreateRequestDto>().ReverseMap();            
            this.CreateMap<CatalogItem, CatalogItemUpdateRequestDto>().ReverseMap();                        
            this.CreateMap<CatalogCategory, CatalogCategoryResponseDto>().ReverseMap();            
            this.CreateMap<CatalogBrand, CatalogBrandResponseDto>().ReverseMap();            
        }
    }
}
