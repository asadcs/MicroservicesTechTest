using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pixel.Infrastructure.Extensions
{
    public static class MediatRExtensions
    {
        public static void RegisterServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            // Additional custom service registration logic
        }
    }

}
