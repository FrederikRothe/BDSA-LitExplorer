namespace LitExplore.ApplicationLogic;
public class Team
{
    public int Id { get; set; }

    public User TeamLeader { get; set; }
    
    public ICollection<User> Users { get; set; }

    public string TeamName { get; set; }

    public Team(User teamLeader, string teamName)
    {
        TeamLeader = teamLeader;
        Users = new List<User>();
        Users.Add(teamLeader);
        TeamName = teamName;
    }
}