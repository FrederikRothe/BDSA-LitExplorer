using LitExplore.ApplicationLogic;
using Microsoft.EntityFrameworkCore;
namespace ApplicationLogic
{
    public class LitExploreContext : DbContext, ILitExploreContext
    {
        public DbSet<Paper> Papers => Set<Paper>();
        public DbSet<Connection> Connections => Set<Connection>();
        public DbSet<Team> Teams => Set<Team>();

        public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Paper>()
                .HasIndex(p => p.Id)
                .IsUnique();

            modelBuilder
                .Entity<Connection>()
                .HasIndex(con => con.Id)
                .IsUnique();

            modelBuilder
                .Entity<Team>()
                .HasIndex(team => team.Id)
                .IsUnique();
        }

        public int saveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

