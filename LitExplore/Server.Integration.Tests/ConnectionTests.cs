namespace Server.Integration.Tests;

public class ConnectionTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    private TestClaimsProvider _provider;

    private HttpClient _client;

    public ConnectionTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _provider = TestClaimsProvider.WithUserClaims();
        _client = _factory.CreateClientWithTestAuth(_provider);
    }

    [Fact]
    public async Task Get_returns_connection_with_id_1()
    {
        var connection = await _client.GetFromJsonAsync<ConnectionDTO>("api/Connection/1");

        Assert.NotNull(connection);
        if (connection != null)
        {
            Assert.True(connection.ConnectionType == "Math");
            Assert.True(connection.Description == "1");
        }
    }

    [Fact]
    public async Task Delete_returns_notfound_with_non_excisting_id_720()
    {
        var response = await _client.DeleteAsync("api/Connection/720");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_returns_Created()
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
        var response = await _client.PostAsJsonAsync("api/Connection", connection);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_returns_newly_created_connection()
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
        var response = await _client.PostAsJsonAsync("api/Connection", newConnection);

        var connection = await _client.GetFromJsonAsync<ConnectionDTO>("api/Connection/4");

        Assert.NotNull(connection);
        if (connection != null)
        {
            Assert.Equal("1", connection.CreatorId);
            Assert.Equal("Fish n' Chips", connection.ConnectionType);
        }
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

        var connection = await _client.PutAsJsonAsync("api/Connection/3", update);
        var updatedConnection = await _client.GetFromJsonAsync<ConnectionDTO>("api/Connection/3");

        Assert.NotNull(updatedConnection);
        if (updatedConnection != null) 
        {
            Assert.Equal("3", updatedConnection.CreatorId);
            Assert.Equal("Cooking", updatedConnection.ConnectionType);
        }
    }

    [Fact]
    public async Task Delete_returns_NoContent_when_successfull()
    {
        var connection = await _client.DeleteAsync("api/Connection/3");

        Assert.Equal(HttpStatusCode.NoContent, connection.StatusCode);
    }
}   