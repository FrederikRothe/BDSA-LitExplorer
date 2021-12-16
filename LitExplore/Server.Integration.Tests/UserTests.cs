namespace Server.Integration.Tests;

public class UserTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private TestClaimsProvider _provider;
    private HttpClient _client;
    public UserTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _provider = TestClaimsProvider.WithUserClaims();
        _client = _factory.CreateClientWithTestAuth(_provider);
    }

    [Fact]
    public async Task get_returns_users()
    {
        var user = await _client.GetFromJsonAsync<UserDTO>("api/User/1");

        Assert.NotNull(user);
        if (user != null) 
        {
            Assert.True(user.Name == "Bob");
        }
    }

    [Fact]
    public async Task getConnections_returns_correct_list_of_connections()
    {
        var userConnections = await _client.GetFromJsonAsync<ConnectionDTO[]>("api/User/connections/1");

        Assert.NotNull(userConnections);
        Assert.Contains(userConnections, u => u.ConnectionType == "Math");
    }

    [Fact]
    public async Task getTeams_returns_correct_list_of_teams()
    {
        var userTeams = await _client.GetFromJsonAsync<TeamDTO[]>("api/User/teams/1");

        Assert.NotNull(userTeams);
        Assert.Contains(userTeams, u => u.TeamName == "Potato");
    }

}   