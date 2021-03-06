namespace LitExplore.Infrastructure;

public class LitExploreContext : DbContext, ILitExploreContext
{
    public DbSet<Paper> Papers => Set<Paper>();

    public DbSet<Connection> Connections => Set<Connection>();

    public DbSet<Team> Teams => Set<Team>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Tag> Tags => Set<Tag>();

    public LitExploreContext(DbContextOptions<LitExploreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasOne(t => t.TeamLeader).WithMany(u => u.IsLeaderOf).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Team>().HasMany(t => t.Users).WithMany(u => u.Teams);
        
        modelBuilder.Entity<Connection>().HasOne(c => c.Creator).WithMany(u => u.Connections).IsRequired(false);
        modelBuilder.Entity<Connection>().HasOne(c => c.Paper1).WithMany(p => p.AsPaper1).HasForeignKey(c => c.Paper1Id).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Connection>().HasOne(c => c.Paper2).WithMany(p => p.AsPaper2).HasForeignKey(c => c.Paper2Id).OnDelete(DeleteBehavior.Restrict);
    }   
}


