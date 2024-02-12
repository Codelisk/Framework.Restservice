using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Framework.Restservice.Server.Extensions;
using AutoMapper;

namespace Framework.Restservice.Server
{
    public static class ProgramHelper
    {

        public static void ConfigureAllServices<TProfile, TDbContext>(this IServiceCollection services) where TDbContext : DbContext where TProfile : Profile, new() 
        {
            services.AddAutoMapper<TProfile>();
            services.AddCors();
            services.AddControllers();
            services.AddIdentity<TDbContext>();
            // ConfigureAndStartApp the RouteOptions to use lowercase URLs
            //services.ConfigureAndStartApp<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddSwagger();
        }

        public static void ConfigureAndStartApp(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.AddIdentity();
            app.UseHttpsRedirection();


            app.MapControllers();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowedToAllowWildcardSubdomains());

            app.Run();
        }

        private static void AddAutoMapper<TProfile>(this IServiceCollection services) where TProfile : Profile, new()
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.AddProfile<TProfile>();
                });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        private static void AddSwagger(this IServiceCollection services)
        {
            // SwaggerGen setup
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt => {
                opt.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
});
            });
        }
    }
}
