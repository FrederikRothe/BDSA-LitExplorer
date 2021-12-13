namespace Server.Integration.Tests;

public class ConnectionTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public ConnectionTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task get_returns_connection_with_id_1()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var connection = await client.GetFromJsonAsync<ConnectionDTO>("/api/Connection/1");


        Assert.NotNull(connection);
        Assert.True(connection.ConnectionType == "Math");
        Assert.True(connection.Description == "1");
    }

    [Fact]
    public async Task delete_returns_notfound_with_non_excisting_id_720()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.DeleteAsync("/api/Connection/720");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
 

}