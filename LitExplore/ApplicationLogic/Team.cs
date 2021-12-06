namespace LitExplore.ApplicationLogic;
public class Team
{
    public int Id { get; set; }
    [Required]
    public User TeamLeader { get; set; }
    public int Colour { get; set; } = 1;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    [Required]
    [StringLength(25)]
    public string TeamName { get; set; }


}