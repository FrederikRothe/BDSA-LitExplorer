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
            Colour = 1,
        };

        var created = await _repository.CreateAsync(team, "1");

        Assert.Equal(4, created.Id);
        Assert.Equal(team.TeamName, created.TeamName);
        Assert.Equal(team.Colour, created.Colour);
        Assert.Equal(team.TeamLeaderId, created.TeamLeaderId);
        Assert.Equal(new List<int>{1}, created.UserIDs);
        Assert.Equal(new List<int>(), created.ConnectionIDs);
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