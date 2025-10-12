using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Mappers.AutoMapperProfiles;
using Web.Application.Repositories;
using Web.Application.Services;
using Web.Application.Validatiors;
using Web.Domain.Repositories;
using Web.Persistence.Catalog;

namespace Web.Application
{
    public static class ApplicationServiceExtensions
    {       
        public static void AddCatalogApiClientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICatalogApiClientService, CatalogApiClientService>();
        }      
        public static void AddCatalogServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICatalogBrandRepositoryAsync, CatalogBrandRepository>();
            serviceCollection.AddScoped<ICatalogCategoriesRepositoryAsync, CatalogCategoriesRepository>();
            serviceCollection.AddScoped<ICatalogRepositoryAsync, CatalogRepositoryAsync>();
            serviceCollection.AddScoped<ICatalogService, CatalogService>();            
            serviceCollection.AddScoped<IPictureService, PictureService>();            
            serviceCollection.AddScoped<IValidator<GetItemsFilterDto>, GetItemsFilterDtoValidator>();
        }      
        //public static void AddCatalogMapper(this IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddAutoMapper(typeof(DefaultAutoMapperProfile));
        //}
    }
}
