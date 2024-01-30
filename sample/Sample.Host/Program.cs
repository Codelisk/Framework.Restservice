
namespace Sample.Host
{
    public partial class Program : BaseProgram
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public override void ConfigureDatabase(WebApplicationBuilder webApplicationBuilder)
        {
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<BackendContext>(opt =>
            //{
            //    opt.UseMySql(connectionString, new MariaDbServerVersion("15.1"), x => x.MigrationsAssembly("Backend.Host"));
            //    //opt.UseSqlite(connectionString);
            //});
        }
    }
}