namespace LitExplore.ApplicationLogic;
public class Team
{
    public int Id { get; set; }

    public User? TeamLeader { get; set; }
    public int Colour { get; set; }
    public ICollection<User>? Users { get; set; }
    public ICollection<Connection>? Connections { get; set; }
    [Required]
    [StringLength(25)]
    public string TeamName { get; set; }


}