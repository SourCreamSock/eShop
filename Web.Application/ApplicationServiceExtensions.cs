using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Mappers.AutoMapperProfiles;
using Web.Application.Services;

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
            serviceCollection.AddScoped<ICatalogService, CatalogService>();
            serviceCollection.AddScoped<IPictureService, PictureService>();            
        }
        //public static void AddCatalogMapper(this IServiceCollection serviceCollection)
        //{
        //    serviceCollection.AddAutoMapper(typeof(DefaultAutoMapperProfile));
        //}
    }
}
