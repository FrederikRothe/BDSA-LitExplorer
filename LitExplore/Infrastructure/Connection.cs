namespace LitExplore.Infrastructure;

public class Connection
{
    public int Id { get; set; }
    
    public Paper Paper1 { get; set; }

    public Paper Paper2 { get; set; }

    public string ConnectionType { get; set; }

    public string? Description { get; set; }

    public Connection(Paper paper1, Paper paper2, string connectionType, string? description)
    {
        Paper1 = paper1;
        Paper2 = paper2;
        ConnectionType = connectionType;
        Description = description;
    }
}