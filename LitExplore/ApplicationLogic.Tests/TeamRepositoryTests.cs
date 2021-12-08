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
        var team = new TeamDTO(1, "Potato", 1, 1, new List<int>{1,2}, new List<int>{1,2});

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
        var conns = new List<ConnectionDTO>
        {
            new ConnectionDTO(2, "2", 2, 1, "Science", "2", new List<int>{1,2}), 
            new ConnectionDTO(3, "3", 1, 2, "Physics", "3", new List<int>{2,3}) 
        }.AsReadOnly();
        
        var connections = await _repository.ReadConnectionsAsync(2);

        Assert.Collection(connections, 
            conn => Assert.Equal(JsonConvert.SerializeObject(conns[0]), JsonConvert.SerializeObject(conn)),
            conn => Assert.Equal(JsonConvert.SerializeObject(conns[1]), JsonConvert.SerializeObject(conn))
        );
    }

    [Fact]
    public async Task ReadUsersAsync_returns_all_users_of_a_team_with_a_giving_id()
    {
        var Users = new List<UserDTO>
        {
            new UserDTO(2, "2", "Suzie", new List<int>{2}, new List<int>{1,2}),
            new UserDTO(3, "3", "Robert", new List<int>{3}, new List<int>{2,3})
        }.AsReadOnly();

        var UsersFound = await _repository.ReadUsersAsync(2);

        Assert.Collection(UsersFound,
            user => Assert.Equal(JsonConvert.SerializeObject(Users[0]), JsonConvert.SerializeObject(user)),
            user => Assert.Equal(JsonConvert.SerializeObject(Users[1]), JsonConvert.SerializeObject(user))
        );
    }

    [Fact]
    public async Task ReadUserAsync_returns_empty_user_readonly_list_when_given_invalid_id()
    {
        var empty = new List<UserDTO>().AsReadOnly();

        var returned = await _repository.ReadUsersAsync(4);

        Assert.Equal(empty, returned);

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