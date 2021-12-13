namespace Server.Integration.Tests;

public class PaperTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
     public PaperTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutiRedirect = false
        });
    }

    [Fact]
    public async Task get_returns_papers()
    {
        var papers = await _client.GetFromJsonAsync<PaperDTO[]>("/api/papers");


        Assert.NotNull(papers);
        Assert.True(papers.Length == 2);
        Assert.Contains(papers, c => c.Title == "Obese");
        Assert.Contains(papers, c => c.Title == "Fit");
    }

}