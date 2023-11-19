using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Publisher
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPublisherServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
