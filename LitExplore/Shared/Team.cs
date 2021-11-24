namespace LitExplore.Shared;

public class Team
{
    public int Id { get; set; }
    
    public ICollection<User> Users { get; set; }

    public string TeamName { get; set; }

    public Team(ICollection<User> users, string teamName)
    {
        Users = users;
        TeamName = teamName;
    }
}