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
    [Fact]
    public async Task post_returns_sucess()
    {   
        var connection = new ConnectionCreateDTO
        {
            CreatorId = "25",
            PaperOneId = 22,
            PaperTwoId = 50,
            ConnectionType = "Rock n' Roll",
            Description = "Not much to say",
            TeamId = 145
        };
        
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.PostAsync("/api/Connection",connection);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task post_returns_newly_created_connection()
    {
        var newConnection = new ConnectionCreateDTO
        {
            CreatorId = "25",
            PaperOneId = 22,
            PaperTwoId = 50,
            ConnectionType = "Rock n' Roll",
            Description = "Not much to say",
            TeamId = 145
        };

        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.PostAsJsonAsync("/api/Connection", newConnection);

        var connection = await client.GetFromJsonAsync<ConnectionDTO>("/api/Connection/4");

        Assert.Equal("25", connection.CreatorId);
        Assert.Equal("Rock n' Roll", connection.ConnectionType);

    }
 

}