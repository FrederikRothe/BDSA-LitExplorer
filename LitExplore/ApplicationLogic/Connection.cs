namespace LitExplore.ApplicationLogic;

public class Connection
{
    public int Id { get; set; }

    public User? Creator { get; set; }

    [Required]
    public Paper Paper1 { get; set; } = null!;

    public int Paper1Id { get; set; }

    [Required]
    public Paper Paper2 { get; set; } = null!;

    public int Paper2Id { get; set; }

    public string ConnectionType { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;
    
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}