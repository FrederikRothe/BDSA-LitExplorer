namespace LitExplore.Infrastructure.Tests;

public class ConnectionRepositoryTests : IDisposable 
{
    private readonly ILitExploreContext _context;
    private readonly ConnectionRepository _repository;

    private bool disposed;

    public ConnectionRepositoryTests()
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
        _repository = new ConnectionRepository(_context);
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