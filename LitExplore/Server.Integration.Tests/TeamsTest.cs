namespace Server.Integration.Tests;

public class TeamsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public TeamsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task get_by_id_1_returns_correct_team()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var team = await client.GetFromJsonAsync<TeamDTO>("/api/Team/1");


        Assert.NotNull(team);
        Assert.Equal("Potato", team.TeamName);
    }

    [Fact]
    public async Task post_returns_created()
    {   
        var newTeam = new TeamCreateDTO
        {
            TeamLeaderId = "2",
            TeamName = "Got any games on your phone?",
            Colour = 4
        };
        
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var response = await client.PostAsJsonAsync("api/Team", newTeam);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    
    }
}