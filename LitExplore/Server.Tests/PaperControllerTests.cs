namespace Server.Tests;

public class PaperControllerTests
{

    [Fact]
    public async Task Get_returns_papers_from_repo()
    {
        // Arrange
        var logger = new Mock<ILogger<PaperController>>();
        var expected = new List<PaperDTO>();
        var repository = new Mock<IPaperRepository>();
        repository.Setup(m => m.ReadAsync()).ReturnsAsync(expected);
        var controller = new PaperController(logger.Object, repository.Object);

        // Act
        var actual = await controller.GetPapers();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Get_given_nonexisting_paper_returns_Notresponse()
    {
        // Arrange
        var logger = new Mock<ILogger<PaperController>>();
        var repository = new Mock<IPaperRepository>();
        repository.Setup(m => m.ReadAsync(69)).ReturnsAsync(default(PaperDTO));
        var controller = new PaperController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(69);

        // Assert
        Assert.IsType<NotFoundResult>(response.Result);
    }

    [Fact]
    public async Task Get_given_existing_paper_returns_paper()
    {
        // Arrange
        var logger = new Mock<ILogger<PaperController>>();
        var repository = new Mock<IPaperRepository>();
        var paper = new PaperDTO(1, "document", new List<string>(), "Title", 2021, 12, 24, new List<string>());
        repository.Setup(m => m.ReadAsync(1)).ReturnsAsync(paper);
        var controller = new PaperController(logger.Object, repository.Object);

        // Act
        var response = await controller.Get(1);

        // Assert
        Assert.NotNull(response);
        if (response.Value != null)
        {
            Assert.Equal(paper.Id, response.Value.Id);
            Assert.Equal(paper.Document, response.Value.Document);
            Assert.Equal(paper.AuthorNames, response.Value.AuthorNames);
            Assert.Equal(paper.Title, response.Value.Title);
            Assert.Equal(paper.Year, response.Value.Year);
            Assert.Equal(paper.Month, response.Value.Month);
            Assert.Equal(paper.Day, response.Value.Day);
            Assert.Equal(paper.TagNames, response.Value.TagNames);
        }
    }
}