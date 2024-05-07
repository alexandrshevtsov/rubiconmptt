using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RubiconMp.IntegrationTests.Utility.Fixtures;
using RubiconMp.Web;

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
                var serviceProvider = services.BuildServiceProvider();

                _sqlServerFixture.AddDbContext(services);

                builder.UseSetting(WebHostDefaults.ApplicationKey, typeof(Program).Assembly.FullName)
                       .UseDefaultServiceProvider(options => options.ValidateScopes = true)
                       .ConfigureServices(s => s.AddSingleton(serviceProvider));
            });
        }
    }
}
