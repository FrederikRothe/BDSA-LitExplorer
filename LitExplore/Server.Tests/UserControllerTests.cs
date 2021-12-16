namespace Server.Tests;

public class UserControllerTests
{
    [Fact]
    public async Task Create_creates_user()
    {
        var logger = new Mock<ILogger<UserController>>();
        var toCreate = new UserCreateDTO();
        var created = new UserDTO(1, "userOid", "Name", new List<int>(), new List<int>());
        var repository = new Mock<IUserRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new UserController(logger.Object, repository.Object);

        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }

    [Fact]
    public async Task Get_given_nonexisting_user_returns_NotFound()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        repository.Setup(m => m.ReadAsync("NonExistingUserOid")).ReturnsAsync(default(UserDTO));
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.Get("NonExistingUserOid");

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_user_returns_user()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        var user = new UserDTO(1, "userOid", "Name", new List<int>(), new List<int>());
        repository.Setup(m => m.ReadAsync("userOid")).ReturnsAsync(user);
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.Get("userOid");

        Assert.Equal(user, response.Value);
    }

    [Fact]
    public async Task Get_connections_given_nonexisting_user_returns_NotFound()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        repository.Setup(m => m.ReadConnectionsAsync("NonExistingUserOid")).ReturnsAsync(new List<ConnectionDTO>());
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.GetConnections("NonExistingUserOid");

        Assert.Equal(new List<ConnectionDTO>(), response);
    }

    [Fact]
    public async Task Get_connections_given_existing_userOid_returns_connections()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        var connections = new List<ConnectionDTO>();
        repository.Setup(m => m.ReadConnectionsAsync("UserOid")).ReturnsAsync(connections);
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.GetConnections("UserOid");

        Assert.Equal(connections, response);
    }

    [Fact]
    public async Task Get_teams_given_nonexisting_user_returns_NotFound()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        repository.Setup(m => m.ReadTeamsAsync("NonExistingUserOid")).ReturnsAsync(new List<TeamDTO>());
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.GetTeams("NonExistingUserOid");

        Assert.Equal(new List<TeamDTO>(), response);
    }

    [Fact]
    public async Task Get_teams_given_existing_userOid_returns_teams()
    {
        var logger = new Mock<ILogger<UserController>>();
        var repository = new Mock<IUserRepository>();
        var teams = new List<TeamDTO>();
        repository.Setup(m => m.ReadTeamsAsync("UserOid")).ReturnsAsync(teams);
        var controller = new UserController(logger.Object, repository.Object);

        var response = await controller.GetTeams("UserOid");

        Assert.Equal(teams, response);
    }
}