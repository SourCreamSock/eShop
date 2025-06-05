using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Mappers.AutoMapperProfiles;

namespace Web.Infrastructure.Mappers
{
    public static class MapperServiceExtensions
    {
        public static void AddDefaultMapper(this IServiceCollection serviceCollection)
        { 
            serviceCollection.AddAutoMapper(typeof(DefaultAutoMapperProfile));
        }
    }
}
