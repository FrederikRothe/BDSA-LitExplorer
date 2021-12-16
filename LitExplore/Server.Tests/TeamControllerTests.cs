namespace Server.Tests;

public class TeamControllerTests
{
    [Fact]
    public async Task Create_creates_team()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var toCreate = new TeamCreateDTO();
        var created = new TeamDTO(1, "TeamName", 1, "TeamLeader-oid", new List<string>(), new List<int>());
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new TeamController(logger.Object, repository.Object);

        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }

    [Fact]
    public async Task Get_given_nonexisting_team_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ReadAsync(69)).ReturnsAsync(default(TeamDTO));
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Get(69);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_team_returns_team()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        var team = new TeamDTO(1, "TeamName", 1, "TeamLeader-oid", new List<string>(), new List<int>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(team);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Get(1);

        Assert.Equal(team, response.Value);
    }

    [Fact]
    public async Task Get_connections_given_nonexisting_team_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ReadConnectionsAsync(69)).ReturnsAsync(new List<ConnectionDTO>());
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.GetTeamConnections(69);

        Assert.Equal(new List<ConnectionDTO>(), response);
    }

    [Fact]
    public async Task Get_connections_given_existing_team_returns_connections()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        var connections = new List<ConnectionDTO>();
        repository.Setup(m => m.ReadConnectionsAsync(1)).ReturnsAsync(connections);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.GetTeamConnections(1);

        Assert.Equal(connections, response);
    }

    [Fact]
    public async Task Get_users_given_nonexisting_team_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ReadUsersAsync(69)).ReturnsAsync(new List<UserDTO>());
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.GetTeamUsers(69);

        Assert.Equal(new List<UserDTO>(), response);
    }

    [Fact]
    public async Task Get_users_given_existing_team_returns_users()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        var users = new List<UserDTO>();
        repository.Setup(m => m.ReadUsersAsync(1)).ReturnsAsync(users);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.GetTeamUsers(1);

        Assert.Equal(users, response);
    }

    [Fact]
    public async Task Put_updates_team()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var team = new TeamUpdateDTO();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.UpdateAsync(1, team)).ReturnsAsync(Updated);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Put(1, team);

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Put_given_nonexisting_teamId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var team = new TeamUpdateDTO();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.UpdateAsync(69, team)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Put(69, team);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_user_given_nonexisting_teamId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.AddUserToTeamAsync(69, "userOid")).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.AddUser(69, "userOid");

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_user_given_nonexisting_userId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.AddUserToTeamAsync(1, "NonExisting-userOid")).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.AddUser(1, "NonExisting-userOid");

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_user_given_existing_teamId_userId_adds_user()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.AddUserToTeamAsync(1, "userOid")).ReturnsAsync(Updated);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.AddUser(1, "userOid");

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Put_connection_given_nonexisting_teamId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ShareConnectionAsync(69, 1)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.ShareConnection(69, 1);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_connection_given_nonexisting_connectionId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ShareConnectionAsync(1, 69)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.ShareConnection(1, 69);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Put_connection_given_existing_teamId_connectionId_adds_connection()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.ShareConnectionAsync(1, 1)).ReturnsAsync(Updated);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.ShareConnection(1, 1);

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_given_nonexisting_team_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.DeleteAsync(69)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Delete(69);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_existing_team_returns_NoContent()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Deleted);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.Delete(1);

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_user_given_nonexisting_teamId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveUserAsync(69, "userOid")).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveUser(69, "userOid");

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_user_given_nonexisting_userId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveUserAsync(1, "NonExisting-userOid")).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveUser(1, "NonExisting-userOid");

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_user_given_existing_teamId_userId_removes_user()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveUserAsync(1, "userOid")).ReturnsAsync(Updated);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveUser(1, "userOid");

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Delete_connection_given_nonexisting_teamId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveConnectionAsync(69, 1)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveConnection(69, 1);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_connection_given_nonexisting_connectionId_returns_NotFound()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveConnectionAsync(1, 69)).ReturnsAsync(NotFound);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveConnection(1, 69);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_connection_given_existing_teamId_connectionId_removes_connection()
    {
        var logger = new Mock<ILogger<TeamController>>();
        var repository = new Mock<ITeamRepository>();
        repository.Setup(m => m.RemoveConnectionAsync(1, 1)).ReturnsAsync(Updated);
        var controller = new TeamController(logger.Object, repository.Object);

        var response = await controller.RemoveConnection(1, 1);

        Assert.IsType<NoContentResult>(response);
    }
}