using Framework.Restservice.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Restservice.Server.Extensions
{
    public static class IdentityServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<UserDto>()
                .AddEntityFrameworkStores<Framework.RestserviceContext>()
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
