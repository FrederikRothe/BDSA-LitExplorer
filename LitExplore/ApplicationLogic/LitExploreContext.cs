namespace LitExplore.ApplicationLogic;
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

    }

    public int saveChanges()
    {
        throw new NotImplementedException();
    }
}


