namespace LitExplore.ApplicationLogic;

public class Connection
{
    public int Id { get; set; }
    
    public Paper Paper1 { get; set; }

    public Paper Paper2 { get; set; }

    public ICollection<ConnectionType> Connections { get; set; }

    public string? Description { get; set; }

    public Connection(Paper paper1, Paper paper2, ICollection<ConnectionType> connectionTypes, string? description)
    {
        Paper1 = paper1;
        Paper2 = paper2;
        Connections = connectionTypes;
        Description = description;
    }
}