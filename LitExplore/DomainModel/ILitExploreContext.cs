namespace LitExplore.DomainModel;

public interface ILitExploreContext : IDisposable
{
    DbSet<Paper> Papers { get; }
    DbSet<Connection> Connections { get; }
    DbSet<Team> Teams { get; }
    DbSet<User> Users { get ; }
    DbSet<Author> Authors { get; }
    DbSet<Tag> Tags { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

