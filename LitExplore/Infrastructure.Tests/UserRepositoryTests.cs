namespace LitExplore.Infrastructure.Tests;
public class UserRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly UserRepository _repository;

    private bool disposed;

    public UserRepositoryTests()
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
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_creates_new_user_with_generated_id()
    {
        var user = new UserCreateDTO
        {
            oid = "Robert",
            Name = "Robert Jr."
        };

        var created = await _repository.CreateAsync(user);

        Assert.Equal(4, created.Id);
        Assert.Equal(user.oid, created.oid);
        Assert.Equal(user.Name, created.Name);
        Assert.Equal(new List<int>(), created.ConnectionIDs);
        Assert.Equal(new List<int>(), created.TeamIDs);
    }

    [Fact]
    public async Task CreateAsync_throws_exception_if_trying_to_create_user_with_no_object()
    {
        await Assert.ThrowsAsync<Exception>(async () => await _repository.CreateAsync(null));
    }

    [Fact]
    public async Task ReadAsync_given_valid_user_id_returns_user_with_given_id()
    {
        var user = new UserDTO(2, "2", "Suzie", new List<int>{2}, new List<int>{1,2});
        
        var found = await _repository.ReadAsync("2");
    
        Assert.Equal(user.Id, found.Value.Id);
        Assert.Equal(user.oid, found.Value.oid);
        Assert.Equal(user.Name, found.Value.Name);
        Assert.Equal(user.ConnectionIDs, found.Value.ConnectionIDs);
        Assert.Equal(user.TeamIDs, found.Value.TeamIDs);
    }

    [Fact]
    public async Task ReadAsync_given_invalid_user_id_returns_null()
    {
        var found = await _repository.ReadAsync("7");

        Assert.True(found.IsNone);
    }

    [Fact]
    public async Task ReadConnectionsAsync_given_valid_user_id_returns_all_connections_of_the_user()
    {
        var conns = new List<ConnectionDTO>
        {
            new ConnectionDTO(3, "3", 1, 2, "Physics", "3", new List<int>{2,3})
        }.AsReadOnly();

        var connections = await _repository.ReadConnectionsAsync("3");

        Assert.Collection(connections,
            conn => Assert.Equal(JsonConvert.SerializeObject(conns[0]), JsonConvert.SerializeObject(conn))
        );
    }

    [Fact]
    public async Task ReadConnectionsAsync_given_invalid_user_id_returns_empty_readonly_collection()
    {
        var empty = new List<ConnectionDTO>().AsReadOnly();

        var found = await _repository.ReadConnectionsAsync("10");

        Assert.Equal(empty, found);
    }

    [Fact]
    public async Task ReadTeamsAsync_given_valid_user_id_returns_all_joined_teams_of_the_user()
    {
        var actual = new List<TeamDTO>
        {
            new TeamDTO(2, "Orange", 2, "2", new List<string>{"2", "3"}, new List<int>{2,3}),
            new TeamDTO(3, "Candy", 3, "3", new List<string>{"3"}, new List<int>{3})
        }.AsReadOnly();

        var teams = await _repository.ReadTeamsAsync("3");

        Assert.Collection(teams,
            team => Assert.Equal(JsonConvert.SerializeObject(actual[0]), JsonConvert.SerializeObject(team)),
            team => Assert.Equal(JsonConvert.SerializeObject(actual[1]), JsonConvert.SerializeObject(team))
        );
    }

    [Fact]
    public async Task ReadTeamsAsync_given_invalid_user_id_returns_empty_readonly_collection()
    {
        var empty = new List<TeamDTO>().AsReadOnly();

        var found = await _repository.ReadTeamsAsync("10");

        Assert.Equal(empty, found);
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