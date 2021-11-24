namespace LitExplore.Shared;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public ICollection<Connection> Connections { get; set; }
}