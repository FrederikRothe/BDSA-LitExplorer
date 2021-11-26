using LitExplore.ApplicationLogic;
using Microsoft.EntityFrameworkCore;

namespace LitExplore.ApplicationLogic {

    public interface ILitExploreContext : IDisposable
    {
        DbSet<Paper> Papers { get; }
        DbSet<Connection> Connections { get; }
        DbSet<Team> Teams { get; }

        int saveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}