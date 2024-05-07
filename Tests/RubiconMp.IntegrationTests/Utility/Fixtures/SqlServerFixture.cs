using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RubiconMp.Data;
using Testcontainers.MsSql;

namespace RubiconMp.IntegrationTests.Utility.Fixtures
{
    public class SqlServerFixture : IAsyncLifetime
    {
        private MsSqlContainer _msSqlContainer;
        public string ConnectionString { get; private set; }
        public DbContextOptions<ApplicationContext> DbContextOptions { get; private set; }

        public async Task DisposeAsync()
        {
            await _msSqlContainer.StopAsync();
            await _msSqlContainer.DisposeAsync().AsTask();
        }

        public async Task InitializeAsync()
        {
            _msSqlContainer = new MsSqlBuilder().Build();

            await _msSqlContainer.StartAsync();

            ConnectionString = _msSqlContainer.GetConnectionString();
            DbContextOptions = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(ConnectionString).Options;

            var services = new ServiceCollection();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(ConnectionString, b => b.UseNetTopologySuite()),
                ServiceLifetime.Transient
            );

            var serviceProvider = services.BuildServiceProvider();

            DbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();
        }

        public IServiceCollection AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(ConnectionString, b => b.UseNetTopologySuite()),
                ServiceLifetime.Transient
            );
            return services;
        }
    }
}
