using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Restservice.Repositories
{
    public static partial class ModuleInitializer
    {
        public static void AddBaseServices(this IServiceCollection services)
        {
            services.AddScoped<DefaultRepositoryProvider>();
        }
    }
}
