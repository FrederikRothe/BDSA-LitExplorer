namespace LitExplore.ApplicationLogic;
public class User
{
    // This is just to keep oid's for the AAD shit
    public int Id { get; set; }

    public string oid { get; init; }

    public string Name { get; init; }

    public ICollection<Team>? IsLeaderOf { get; set; }

    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}