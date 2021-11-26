namespace ApplicationLogic;

public class LitExplorerContext : DbContext, ILitExplorerContext
{
    public DbSet<Paper> Papers => Set<Papers>();
    public DbSet<Connection> Connections => Set<Connection>();
    public DbSet<Team> Teams => Set<Teams>();

    public LitExplorerContext(DbContextOptions<LitExplorerContext> options) : base(options) { }

    protected overrride void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Paper>()
            .HasIndex(p => p.Id)
            .isUnique();

        modelBuilder
            .Entity<Connection>()
            .HasIndex(con => con.Id)
            .isUnique();

        modelBuilder
            .Entity<Team>()
            .HasIndex(team => team.Id)
            .isUnique();
    }
}