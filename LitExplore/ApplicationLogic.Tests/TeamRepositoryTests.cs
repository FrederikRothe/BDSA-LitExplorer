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
    public async Task CreateAsync_creates_team_with_generated_id()
    {   
        var UIDs = new List<int>{};
        var CIDs = new List<int>{};

        var team = new TeamCreateDTO 
        {
           TeamLeaderId = 1,
           TeamName = "Popsicle",
           Colour = 5
        };

        var created = await _repository.CreateAsync(team, "1");

        Assert.Equal(4, created.Id);
        Assert.Equal(1, created.TeamLeaderId);
        Assert.Equal(5, created.Colour);
        Assert.Equal(UIDs, created.UserIDs);
        Assert.Equal(CIDs, created.ConnectionIDs);
        Assert.Equal("Popsicle", created.TeamName);
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