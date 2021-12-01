



namespace LitExplore.ApplicationLogic;
public class Team
{
    public int Id { get; set; }

    [Required]
    public User TeamLeader { get; set; }
    
    public ICollection<User> Users { get; set; }

    [Required]
    [StringLength(25)]
    public string TeamName { get; set; }


}