namespace Server.Integration.Tests;

public class PaperTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    public PaperTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task get_returns_papers()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var papers = await client.GetFromJsonAsync<PaperDTO[]>("/api/Paper");


        Assert.NotNull(papers);
        Assert.True(papers.Length == 2);
        Assert.Contains(papers, c => c.Title == "Obese");
        Assert.Contains(papers, c => c.Title == "Fit");
    }

    [Fact]
    public async Task get_returns_papers_with_correct_document()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        var papers = await client.GetFromJsonAsync<PaperDTO[]>("/api/Paper");


        Assert.Contains(papers, c => c.Document == "2");
    }

    //Testing that it is in fact not possible to create papers via. the API :-)
    [Fact]
    public async Task Paper__post_returns_Created_with_location()
    {
        var provider = TestClaimsProvider.WithUserClaims();
        var client = _factory.CreateClientWithTestAuth(provider);
        
        var christmasPaper = new PaperCreateDTO
        {
            Document = "3",
            AuthorNames = new List<string> {"Lil Baby", "Drake"},
            Title = "A Christmas Story",
            Year = 2021,
            Month = 12,
            Day = 24,
            TagNames = new List<string> {"Free Smoke", "Something to prove"}
        };

        var response = await client.PostAsJsonAsync("/api/Paper", christmasPaper);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}