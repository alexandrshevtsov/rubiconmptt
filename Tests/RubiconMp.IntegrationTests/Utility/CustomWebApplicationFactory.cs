using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RubiconMp.IntegrationTests.Utility.Fixtures;

namespace RubiconMp.IntegrationTests.Utility
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly SqlServerFixture _sqlServerFixture;

        public CustomWebApplicationFactory()
        {
            _sqlServerFixture = new SqlServerFixture();
            _sqlServerFixture.InitializeAsync().GetAwaiter().GetResult();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //// Register the contexts
                //var existingDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PostgresContext>));
                //if (existingDescriptor != null)
                //{
                //    services.Remove(existingDescriptor);
                //}
                //services.AddDbContext<PostgresContext>(options =>
                //    options.UseNpgsql(_postgreSqlFixture.ConnectionString),
                //    ServiceLifetime.Transient
                //);

                //// Register the contexts
                //_postgreSqlFixture.AddDbContext(services);
                //_sqlServerFixture.AddDbContext(services);

                // Add any other necessary service configurations

                // Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                // Set the service provider of the WebApplicationFactory
                builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).Assembly.FullName)
                       .UseDefaultServiceProvider(options => options.ValidateScopes = true)
                       .ConfigureServices(s => s.AddSingleton(serviceProvider));
            });
        }
    }
}
