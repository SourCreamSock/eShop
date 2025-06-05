using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Contracts;
using Web.Application.DTOs.Catalog;
using Web.Application.Services;

namespace Web.Application
{
    public static class ApplicationServiceExtensions
    {
        private static void AddGeneralServices()
        {

        }
        public static void AddApplicationServicesForWeb(this IServiceCollection serviceCollection)
        {
            
        }
        public static void AddApplicationServicesForApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPictureService, PictureService>();
        }
        public static void AddAutoMapper()
        {

        }
    }
}
