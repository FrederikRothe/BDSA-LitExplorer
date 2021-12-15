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
        var connection = await client.GetFromJsonAsync<ConnectionDTO>("api/Connection/1");


        Assert.NotNull(connection);
        Assert.True(connection.ConnectionType == "Math");
        Assert.True(connection.Description == "1");
    }

    [Fact]
    public async Task delete_returns_notfound_with_non_excisting_id_720()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.DeleteAsync("api/Connection/720");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task post_returns_Created()
    {   
        var connection = new ConnectionCreateDTO
        {
            CreatorId = "1",
            PaperOneId = 1,
            PaperTwoId = 2,
            ConnectionType = "Rock n' Roll",
            Description = "Not much to say",
            TeamId = null
        };
        
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.PostAsJsonAsync("api/Connection", connection);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task post_returns_newly_created_connection()
    {
        var newConnection = new ConnectionCreateDTO
        {
            CreatorId = "1",
            PaperOneId = 2,
            PaperTwoId = 1,
            ConnectionType = "Fish n' Chips",
            Description = "Alot to say",
            TeamId = null
        };

        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.PostAsJsonAsync("api/Connection", newConnection);

        var connection = await client.GetFromJsonAsync<ConnectionDTO>("api/Connection/4");

        Assert.Equal("1", connection.CreatorId);
        Assert.Equal("Fish n' Chips", connection.ConnectionType);
    }

    [Fact]
    public async Task Update_updates_connection_and_get_returns_correctly_updated_connection()
    {
        var update = new ConnectionUpdateDTO
        {
            Id = 3,
            CreatorId = "3",
            PaperOneId = 2,
            PaperTwoId = 1,
            ConnectionType = "Cooking",
            Description = "Space Cooking in space",
            TeamIDs = new List<int>()
        };

        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var connection = await client.PutAsJsonAsync("api/Connection/3", update);

        var updatedConnection = await client.GetFromJsonAsync<ConnectionDTO>("api/Connection/3");

        Assert.Equal("3", updatedConnection.CreatorId);
        Assert.Equal("Cooking", updatedConnection.ConnectionType);
    }

    [Fact]
    public async Task Delete_returns_NoContent_when_successfull()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var connection = await client.DeleteAsync("api/Connection/3");

        Assert.Equal(HttpStatusCode.NoContent, connection.StatusCode);
    }
 

}   