
using Framework.Restservice.Server;
public abstract class BaseProgram
{
    public abstract void ConfigureDatabase(WebApplicationBuilder webApplicationBuilder);
    public void FinishSetup<TProfile>(string[] args) where TProfile : Profile, new()
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureDatabase(builder);

        builder.Services.ConfigureAllServices<TProfile>();
        builder.Build().ConfigureAndStartApp();
    }
}
