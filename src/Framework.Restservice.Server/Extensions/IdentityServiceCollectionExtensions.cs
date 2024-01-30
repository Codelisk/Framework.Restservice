using Framework.Restservice.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Restservice.Server.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {
        public static void AddIdentity<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddIdentityCore<UserDto>()
                .AddEntityFrameworkStores<TDbContext>()
                .AddApiEndpoints();

            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);
            services.AddAuthorizationBuilder();
        }

        public static void AddIdentity(this WebApplication app)
        {
            app.MapIdentityApi<UserDto>();
        }
    }
}
