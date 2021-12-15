namespace Server.Integration.Tests;

public class UserTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public UserTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }


    [Fact]
    public async Task get_returns_users()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var user = await client.GetFromJsonAsync<UserDTO>("api/User/1");


        Assert.NotNull(user);
        if (user != null) 
        {
            Assert.True(user.Name == "Bob");
        }
    }

    [Fact]
    public async Task getConnections_returns_correct_list_of_connections()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var userConnections = await client.GetFromJsonAsync<ConnectionDTO[]>("api/User/connections/1");


        Assert.NotNull(userConnections);
        Assert.Contains(userConnections, u => u.ConnectionType == "Math");
    }

    [Fact]
    public async Task getTeams_returns_correct_list_of_teams()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var userTeams = await client.GetFromJsonAsync<TeamDTO[]>("api/User/teams/1");

        Assert.NotNull(userTeams);
        Assert.Contains(userTeams, u => u.TeamName == "Potato");
    }

}   