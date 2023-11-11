using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProductCatalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(
                 config =>
                 {
                     config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                     config.AddProfile(new AssemblyMappingProfile(typeof(IProductDbContext).Assembly));
                 });

            return services;
        }
    }
}
