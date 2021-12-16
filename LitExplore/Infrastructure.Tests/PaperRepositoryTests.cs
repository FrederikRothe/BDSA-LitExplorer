namespace LitExplore.Infrastructure.Tests;

public class PaperRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly PaperRepository _repository;

    private bool _disposed;

    public PaperRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<LitExploreContext>();
        builder.UseSqlite(connection);
        var context = new LitExploreContext(builder.Options);
        context.Database.EnsureCreated();

        context = SeedInMemoryDB(context);

        _context = context;
        _repository = new PaperRepository(_context);
    }

    [Fact]
    public async Task ReadAsync_given_valid_paper_id_returns_paper_with_given_id()
    {
        var paper = new PaperDTO(1, "1", new List<string>{"Bob", "Suzie"}, "Fit", 2000, 1, 1, new List<string>{"Health"});

        var found = await _repository.ReadAsync(1);

        Assert.Equal(paper.Id, found.Value.Id);
        Assert.Equal(paper.Document, found.Value.Document);
        Assert.Equal(paper.AuthorNames, found.Value.AuthorNames);
        Assert.Equal(paper.Title, found.Value.Title);
        Assert.Equal(paper.Year, found.Value.Year);
        Assert.Equal(paper.Month, found.Value.Month);
        Assert.Equal(paper.Day, found.Value.Day);
        Assert.Equal(paper.TagNames, found.Value.TagNames);
    }

    [Fact]
    public async Task ReadAsync_given_invalid_id_returns_null()
    {
        var found = await _repository.ReadAsync(6);

        Assert.True(found.IsNone);
    }

    [Fact]
    public async Task ReadAsync_with_no_parameter_returns_all_papers()
    {
        var papers = new List<PaperDTO>
        {
            new PaperDTO(1, "1", new List<string>{"Bob", "Suzie"}, "Fit", 2000, 1, 1, new List<string>{"Health"}),
            new PaperDTO(2, "2", new List<string>{"Robert"}, "Obese", 2000, 1, 1, new List<string>{"Health"})
        }.AsReadOnly();

        var found = await _repository.ReadAsync();

        Assert.Collection(found,
            paper => Assert.Equal(JsonConvert.SerializeObject(papers[0]), JsonConvert.SerializeObject(paper)),
            paper => Assert.Equal(JsonConvert.SerializeObject(papers[1]), JsonConvert.SerializeObject(paper))
        );
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