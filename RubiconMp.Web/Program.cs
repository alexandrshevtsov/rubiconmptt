using Microsoft.EntityFrameworkCore;
using RubiconMp.Core.Data;
using RubiconMp.Data;
using RubiconMp.Services.Queries;

namespace RubiconMp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Add services to the container.

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetIntersectedRectanglesQuery).Assembly));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString, b => b.UseNetTopologySuite()));

            services.AddTransient(typeof(IRepositoryKeyInteger<>), typeof(RepositoryKeyInteger<>));

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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
