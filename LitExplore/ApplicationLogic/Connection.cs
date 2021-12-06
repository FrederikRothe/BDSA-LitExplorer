namespace LitExplore.ApplicationLogic;

public class Connection
{
    public int Id { get; set; }
    public User? Creator { get; set; }
    public Paper? Paper1 { get; set; }
    [Required]
    public Paper? Paper2 { get; set; }
    [Required]
    public string ConnectionType { get; set; }
    [StringLength(100)]
    public string Description { get; set; }
    public ICollection<Team> Teams { get; set; }
}