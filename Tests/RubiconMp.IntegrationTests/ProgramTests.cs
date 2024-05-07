using RubiconMp.IntegrationTests.Utility;

namespace RubiconMp.IntegrationTests
{
    public class ProgramTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        //private readonly PostgreSqlFixture _postgreSqlFixture;

        public ProgramTests(CustomWebApplicationFactory factory /*, PostgreSqlFixture postgreSqlFixture*/)
        {
            _factory = factory;
            //_postgreSqlFixture = postgreSqlFixture;
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
            //Assert.NotEmpty(_postgreSqlFixture.ConnectionString);
        }
    }
}