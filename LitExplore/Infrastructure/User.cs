namespace LitExplore.Infrastructure;

public class User
{
    // This is just to keep oid's for the AAD shit
    public int Id { get; set; }

    public ICollection<Connection> Connections { get; set; }
}