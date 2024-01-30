
using Framework.Restservice.Server;
using Microsoft.AspNetCore.Builder;
public abstract class BaseProgram
{
    public abstract void ConfigureDatabase(WebApplicationBuilder webApplicationBuilder);
    public void FinishSetup<TProfile, TDbContext>(string[] args) where TProfile : Profile, new() where TDbContext : DbContext
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureDatabase(builder);

        builder.Services.ConfigureAllServices<TProfile, TDbContext>();
        builder.Build().ConfigureAndStartApp();
    }
}
