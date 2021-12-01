namespace LitExplore.ApplicationLogic
{
 /*    public class LitExploreContextFactory : IDesignTimeDbContextFactory<LitExploreContext>
{
    public LitExploreContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("LitExploreDB");
        var optionsBuilder = new DbContextOptionsBuilder<LitExploreContext>()
            .UseSqlServer(connectionString);

        return new LitExploreContext(optionsBuilder.Options);
    }
} */
}
