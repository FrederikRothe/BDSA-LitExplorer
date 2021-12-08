namespace LitExplore.Infrastructure.Tests;

public class TeamRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly TeamRepository _repository;

    private bool disposed;

    public TeamRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<LitExploreContext>();
        builder.UseSqlite(connection);
        var context = new LitExploreContext(builder.Options);
        context.Database.EnsureCreated();

        // Populate In-Memory Database
        context = SeedInMemoryDB(context);

        _context = context;
        _repository = new TeamRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_creates_new_team_with_generated_id() 
    {
        var team = new TeamCreateDTO
        {
            TeamLeaderId = 1,
            TeamName = "Popsicle",
            Colour = 1
        };

        var created = await _repository.CreateAsync(team, "1");

        Assert.Equal(4, created.Id);
        Assert.Equal(team.TeamName, created.TeamName);
        Assert.Equal(team.Colour, created.Colour);
        Assert.Equal(team.TeamLeaderId, created.TeamLeaderId);
        Assert.Equal(new List<int>{1}, created.UserIDs);
        Assert.Equal(new List<int>(), created.ConnectionIDs);
    }

    [Fact] 
    public async Task ReadAsync_returns_team_with_given_id()
    {
        /*var team = new TeamDTO
        {
            Id = 1,
            TeamName = "Potato",
            Colour = 1,
            TeamLeaderId = 1,
            UserIDs = new List<int>{1,2},
            ConnectionIDs = new List<int>{1,3}
        };*/

        var team = new TeamDTO(1, "Potato", 1, 1, new List<int>{1,2}, new List<int>{1,3});

        var found = await _repository.ReadAsync(1);

        found = found.Value;

        Assert.Equal(team.Id, found.Value.Id);
        Assert.Equal(team.TeamName, found.Value.TeamName);
        Assert.Equal(team.Colour, found.Value.Colour);
        Assert.Equal(team.TeamLeaderId, found.Value.TeamLeaderId);
        Assert.Equal(team.UserIDs, found.Value.UserIDs);
        Assert.Equal(team.ConnectionIDs, found.Value.ConnectionIDs);
    }

    [Fact]
    public async Task ReadConnectionsAsync_returns_all_connections_of_a_team_with_given_id()
    {
        var connections = new List<ConnectionDTO>
        {
            new ConnectionDTO(1, "1", 1, 2, "Math", "1", new List<int>{1}),
            new ConnectionDTO(3, "3", 1, 2, "Physics", "3", new List<int>{2,3})
        }.AsReadOnly();
        
        var teams = await _repository.ReadConnectionsAsync(1);

        Assert.Equal(connections, teams);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing) {
                _context.Dispose();
            }

            disposed = true;
        }    
    }  

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}