namespace Server.Integration.Tests;

public class TeamsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    private TestClaimsProvider _provider;

    private HttpClient _client;
    
    public TeamsTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _provider = TestClaimsProvider.WithUserClaims();
        _client = _factory.CreateClientWithTestAuth(_provider);
    }

    [Fact]
    public async Task Get_by_id_1_returns_correct_team()
    {
        var team = await _client.GetFromJsonAsync<TeamDTO>("/api/Team/1");

        Assert.NotNull(team);
        if (team != null) 
        {
            Assert.Equal("Potato", team.TeamName);
        }
    }

    [Fact]
    public async Task Post_succesfully_creates_new_team()
    {   
        var newTeam = new TeamCreateDTO
        {
            TeamLeaderId = "2",
            TeamName = "Got any games on your phone?",
            Colour = 4
        };
    
        var response = await _client.PostAsJsonAsync("api/Team", newTeam);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var team = await _client.GetFromJsonAsync<TeamDTO>("api/Team/4");

        Assert.NotNull(team);
        if (team != null)
        {
            Assert.Equal("2", team.TeamLeaderId);
            Assert.Equal("Got any games on your phone?", team.TeamName);
        }
    }

    [Fact]
    public async Task Delete_returns_notfound_with_non_excisting_id_720()
    {
        var response = await _client.DeleteAsync("api/Team/720");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_returns_NoContent_when_successfull()
    {
        var team = await _client.GetFromJsonAsync<TeamDTO>("/api/Team/1");
        
        Assert.NotNull(team);
        
        var deletedTeam = await _client.DeleteAsync("api/team/1");
        Assert.Equal(HttpStatusCode.NoContent.ToString(), deletedTeam.StatusCode.ToString());
    }

    [Fact]
    public async Task Update_updates_team_and_returns_updated_team()
    {
        var update = new TeamUpdateDTO
        {
            Id = 3,
            TeamLeaderId = "2",
            TeamName = "Dovne Robert blev fyret",
            Colour = 1
        };

        var team = await _client.PutAsJsonAsync("api/Team/3", update);

        var updatedTeam = await _client.GetFromJsonAsync<TeamDTO>("api/Team/3");

        Assert.NotNull(updatedTeam);
        if (updatedTeam != null)
        {
            Assert.Equal("2", updatedTeam.TeamLeaderId);
            Assert.Equal("Dovne Robert blev fyret", updatedTeam.TeamName);
            Assert.Equal(1, updatedTeam.Colour);
        } 
    }    
}