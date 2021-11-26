using LitExplore.ApplicationLogic;

namespace ApplicationLogic {

    public interface ILitExplorerContext : IDisposable
    {
        DbSet<Paper> Papers {get;}
        DbSet<Connection> Connections {get;}
        DbSet<Team> Teams {get;}

        int saveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}