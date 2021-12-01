namespace LitExplore.ApplicationLogic;

public interface ILitExploreContext : IDisposable
{
    DbSet<Paper> Papers { get; }
    DbSet<Connection> Connections { get; }
    DbSet<Team> Teams { get; }
    DbSet<User> Users { get ; }
    DbSet<Author> Authors { get; }
    DbSet<Tag> Tags { get; }

    int saveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

