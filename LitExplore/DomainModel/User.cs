namespace LitExplore.DomainModel;

public class User
{
    public int Id { get; set; }

    [Required]
    public string oid { get; init; } = null!;

    [Required]
    public string Name { get; init; } = null!;

    public ICollection<Team> IsLeaderOf { get; set; } = new List<Team>();

    public ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public ICollection<Team> Teams { get; set; } = new List<Team>();
}