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

}