namespace LitExplore.Infrastructure.Tests;

public class TeamRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly TeamRepository _repository;

    private bool _disposed;

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
            TeamLeaderId = "1",
            TeamName = "Popsicle",
            Colour = 1
        };

        var created = await _repository.CreateAsync(team);

        Assert.Equal(4, created.Id);
        Assert.Equal(team.TeamName, created.TeamName);
        Assert.Equal(team.Colour, created.Colour);
        Assert.Equal(team.TeamLeaderId, created.TeamLeaderId);
        Assert.Equal(new List<string>{"1"}, created.UserIDs);
        Assert.Equal(new List<int>(), created.ConnectionIDs);
    }

    [Fact] 
    public async Task ReadAsync_given_valid_team_id_returns_team()
    {
        var team = new TeamDTO(1, "Potato", 1, "1", new List<string>{"1", "2"}, new List<int>{1,2});

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
    public async Task ReadAsync_given_invalid_team_id_returns_null()
    {
        var found = await _repository.ReadAsync(5);

        Assert.True(found.IsNone);    
    }

    [Fact]
    public async Task ReadConnectionsAsync_given_valid_team_id_returns_all_connections_of_the_team()
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
    public async Task ReadConnectionsAsync_given_invalid_team_id_returns_empty_connection_readonly_list()
    {
        var empty = new List<ConnectionDTO>().AsReadOnly();

        var returned = await _repository.ReadConnectionsAsync(4);

        Assert.Equal(empty, returned);
    }

    [Fact]
    public async Task ReadUsersAsync_given_valid_team_id_returns_all_users_of_the_team()
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
    public async Task ReadUserAsync_returns_given_invalid_id_returns_empty_user_readonly_list()
    {
        var empty = new List<UserDTO>().AsReadOnly();

        var returned = await _repository.ReadUsersAsync(4);

        Assert.Equal(empty, returned);
    }

    [Fact]
    public async Task UpdateAsync_given_valid_team_id_returns_updated_status()
    {
        var update = new TeamUpdateDTO
        {
            Id = 1,
            TeamLeaderId = "1",
            TeamName = "Coca-Cola",
            Colour = 4,
        };

        var response = await _repository.UpdateAsync(1, update);

        Assert.Equal(Updated, response);
    }

    [Fact]
    public async Task UpdateAsync_given_invalid_team_id_returns_notfound_status()
    {
        var update = new TeamUpdateDTO
        {
            Id = 1,
            TeamLeaderId = "1",
            TeamName = "Coca-Cola",
            Colour = 4,
        };

        var response = await _repository.UpdateAsync(5, update);

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task AddUserToTeamAsync_given_valid_input_returns_updated_status()
    {
        var response = await _repository.AddUserToTeamAsync(1, "3");

        Assert.Equal(Updated, response);
    }

    [Fact]
    public async Task AddUserToTeamAsync_given_invalid_input_returns_notfound_status()
    {
        var response = await _repository.AddUserToTeamAsync(4, "3");

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task ShareConnectionAsync_given_valid_input_returns_updated_status()
    {
        var connection = new ConnectionDTO(2, "2", 2, 1, "Science", "2", new List<int>{1,2});
        
        var response = await _repository.ShareConnectionAsync(1, 2);

        Assert.Equal(Updated, response);
    }

    [Fact]
    public async Task ShareConnectionAsync_given_invalid_input_returns_notfound_status()
    {
        var response = await _repository.ShareConnectionAsync(1, 4);

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task DeleteAsync_given_valid_team_id_returns_deleted_status()
    {
        var response = await _repository.DeleteAsync(1);

        Assert.Equal(Deleted, response);
    }

    [Fact]
    public async Task DeleteAsync_given_invalid_team_id_returns_notfound_status()
    {
        var response = await _repository.DeleteAsync(5);

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task RemoveConnectionAsync_given_valid_input_returns_deleted_status()
    {
        var response = await _repository.RemoveConnectionAsync(1, 1);

        Assert.Equal(Deleted, response);
    }

    [Fact]
    public async Task RemoveConnectionAsync_given_invalid_input_returns_notfound_status()
    {
        var response = await _repository.RemoveConnectionAsync(1, 5);

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task RemoveConnectionAsync_given_a_connection_that_is_not_part_of_the_team_returns_badrequest_status()
    {
        var response = await _repository.RemoveConnectionAsync(3, 2);

        Assert.Equal(BadRequest, response);
    }

    [Fact]
    public async Task RemoveUserAsync_given_valid_input_returns_deleted_status()
    {
        var response = await _repository.RemoveUserAsync(1, "1");

        Assert.Equal(Deleted, response);
    }

    [Fact]
    public async Task RemoveUserAsync_given_invalid_input_returns_notfound_status()
    {
        var response = await _repository.RemoveUserAsync(1, "5");

        Assert.Equal(NotFound, response);
    }

    [Fact]
    public async Task RemoveUserAsync_given_a_user_that_is_not_part_of_the_team_returns_badrequest_status()
    {
        var response = await _repository.RemoveUserAsync(3, "2");

        Assert.Equal(BadRequest, response);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) _context.Dispose();

            _disposed = true;
        }    
    }  

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}