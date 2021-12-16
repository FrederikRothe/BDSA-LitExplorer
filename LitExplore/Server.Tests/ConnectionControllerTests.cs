namespace Server.Tests;

public class ConnectionControllerTests
{
    [Fact]
    public async Task Create_creates_connection()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var toCreate = new ConnectionCreateDTO();
        var created = new ConnectionDTO(1, "userOid", 1, 2, "other", "God connection", new List<int>());
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.CreateAsync(toCreate)).ReturnsAsync(created);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var result = await controller.Post(toCreate) as CreatedAtActionResult;

        Assert.Equal(created, result?.Value);
        Assert.Equal("Get", result?.ActionName);
        Assert.Equal(KeyValuePair.Create("Id", (object?)1), result?.RouteValues?.Single());
    }

    [Fact]
    public async Task Get_returns_Connections_from_repo()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var expected = new List<ConnectionDTO>();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.ReadPredefinedAsync()).ReturnsAsync(expected);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var actual = await controller.GetAllConnections();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_given_nonexisting_connection_returns_NotFound()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.ReadAsync(69)).ReturnsAsync(default(ConnectionDTO));
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Get(69);

        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_connection_returns_connection()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var repository = new Mock<IConnectionRepository>();
        var connection = new ConnectionDTO(1, "creator-oid", 1, 2, "other", "description", new List<int>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(connection);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Get(1);

        Assert.Equal(connection, response.Value);
    }

    [Fact]
    public async Task Put_updates_connection()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var connection = new ConnectionUpdateDTO();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.UpdateAsync(1, connection)).ReturnsAsync(Updated);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Put(1, connection);

        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async Task Put_given_nonexisting_connectionId_returns_NotFound()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var connection = new ConnectionUpdateDTO();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.UpdateAsync(69, connection)).ReturnsAsync(NotFound);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Put(69, connection);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_nonexisting_connection_returns_NotFound()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.DeleteAsync(69)).ReturnsAsync(Status.NotFound);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Delete(69);

        Assert.IsType<NotFoundResult>(response);
    }

    [Fact]
    public async Task Delete_given_existing_connection_returns_NoContent()
    {
        var logger = new Mock<ILogger<ConnectionController>>();
        var repository = new Mock<IConnectionRepository>();
        repository.Setup(m => m.DeleteAsync(1)).ReturnsAsync(Status.Deleted);
        var controller = new ConnectionController(logger.Object, repository.Object);

        var response = await controller.Delete(1);

        Assert.IsType<NoContentResult>(response);
    }
}