namespace LitExplore.ApplicationLogic;
public class User
{
    // This is just to keep oid's for the AAD shit
    public string Id { get; init; }

    public string Name { get; init; }

    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}