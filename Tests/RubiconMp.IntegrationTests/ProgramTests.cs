using RubiconMp.IntegrationTests.Utility;
using RubiconMp.IntegrationTests.Utility.Fixtures;

namespace RubiconMp.IntegrationTests
{
    public class ProgramTests : IClassFixture<CustomWebApplicationFactory>, IClassFixture<SqlServerFixture>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly SqlServerFixture _sqlServerFixture;

        public ProgramTests(CustomWebApplicationFactory factory, SqlServerFixture sqlServerFixture)
        {
            _factory = factory;
            _sqlServerFixture = sqlServerFixture;
        }

        [Theory]
        [InlineData("/swagger/v1/swagger.json")]
        public async Task TestEndpointAccess_ReturnSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response?.Content?.Headers?.ContentType?.ToString());
            Assert.NotEmpty(_sqlServerFixture.ConnectionString);
        }
    }
}