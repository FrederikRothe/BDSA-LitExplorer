namespace LitExplore.Infrastructure.Tests;

public class ConnectionRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly ConnectionRepository _repository;

    private bool disposed;

    public ConnectionRepositoryTests()
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
        _repository = new ConnectionRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_creates_new_connection_with_generated_id()
    {
        var connection = new ConnectionCreateDTO
        {
          CreatorId = "3",
          PaperOneId = 2, 
          PaperTwoId = 1,
          ConnectionType = "Food",
          Description = "Meal"  
        };

        var created = await _repository.CreateAsync(connection);

        Assert.Equal(4, created.Id);
        Assert.Equal(connection.CreatorId, created.CreatorId);
        Assert.Equal(connection.PaperOneId, created.PaperOneId);
        Assert.Equal(connection.PaperTwoId, created.PaperTwoId);
        Assert.Equal(connection.ConnectionType, created.ConnectionType);
        Assert.Equal(connection.Description, created.Description);
        Assert.Equal(new List<int>(), created.TeamIDs);
    }

    [Fact]
    public async Task ReadAsync_given_valid_connection_id_returns_connection_with_given_id()
    {
        var connection = new ConnectionDTO(2, "2", 2, 1, "Science", "2", new List<int>{1,2});

        var found = await _repository.ReadAsync(2);

        Assert.Equal(connection.Id, found.Value.Id);
        Assert.Equal(connection.CreatorId, found.Value.CreatorId);
        Assert.Equal(connection.PaperOneId, found.Value.PaperOneId);
        Assert.Equal(connection.PaperTwoId, found.Value.PaperTwoId);
        Assert.Equal(connection.ConnectionType, found.Value.ConnectionType);
        Assert.Equal(connection.Description, found.Value.Description);
        Assert.Equal(connection.TeamIDs, found.Value.TeamIDs);
    }

    [Fact]
    public async Task ReadAsync_given_invalid_connection_id_returns_null()
    {
        var found = await _repository.ReadAsync(5);

        Assert.True(found.IsNone);
    }

    
    [Fact]
    public async Task ReadPredefinedAsync_returns_all_predfined_connections()
    {
        // There are no predefined connections in the in-memory database
        var conns = new List<ConnectionDTO>().AsReadOnly();

        var connections = await _repository.ReadPredefinedAsync();

        Assert.Equal(conns, connections);
    }

    [Fact]
    public async Task ReadTeamsAsync_given_valid_connection_id_returns_all_teams_that_the_connection_is_part_of()
    {
        var teams = new List<TeamDTO>
        {
            new TeamDTO(2, "Orange", 2, "2", new List<string>{"2", "3"}, new List<int>{2,3}),
            new TeamDTO(3, "Candy", 3, "3", new List<string>{"3"}, new List<int>{3})
        }.AsReadOnly();

        var found = await _repository.ReadTeamsAsync(3);

        Assert.Collection(found,
            team => Assert.Equal(JsonConvert.SerializeObject(teams[0]), JsonConvert.SerializeObject(team)),
            team => Assert.Equal(JsonConvert.SerializeObject(teams[1]), JsonConvert.SerializeObject(team))
        );
    }

    [Fact]
    public async Task ReadAsyncTeams_give_invalid_connection_id_returns_empty_list()
    {
        var empty = new List<TeamDTO>().AsReadOnly();

        var found = await _repository.ReadTeamsAsync(7);

        Assert.Equal(empty, found);
    }

    [Fact]
    public async Task UpdateAsync_given_valid_connection_id_returns_updated_status()
    {
        var update = new ConnectionUpdateDTO
        {
            Id = 3,
            CreatorId = "3",
            PaperOneId = 2,
            PaperTwoId = 1,
            ConnectionType = "Astronomy",
            Description = "Space",
            TeamIDs = new List<int>()
        };

        var response = await _repository.UpdateAsync(3, update);

        Assert.Equal(Updated, response);
    }

    [Fact]
    public async Task UpdateAsync_given_invalid_connection_id_returns_notfound_status()
    {
        var update = new ConnectionUpdateDTO
        {
            Id = 3,
            CreatorId = "3",
            PaperOneId = 2,
            PaperTwoId = 1,
            ConnectionType = "Astronomy",
            Description = "Space",
            TeamIDs = new List<int>()
        };

        var response = await _repository.UpdateAsync(6, update);

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task DeleteAsync_given_valid_connection_id_returns_deleted_status()
    {
        var response = await _repository.DeleteAsync(3);

        Assert.Equal(Deleted, response);
    }

    [Fact]
    public async Task DeleteAsync_given_invalid_connection_id_returns_notfound_status()
    {
        var response = await _repository.DeleteAsync(8);

        Assert.Equal(NotFound, response);
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