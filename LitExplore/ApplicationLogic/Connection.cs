namespace LitExplore.ApplicationLogic;

public class Connection
{
    public int Id { get; set; }
    
    public int Paper1 { get; set; }

    public int Paper2 { get; set; }

    public string ConnectionType { get; set; }

    public string? Description { get; set; }

    public Connection(int paper1, int paper2, string connectionType, string? description)
    {
        Paper1 = paper1;
        Paper2 = paper2;
        ConnectionType = connectionType;
        Description = description;
    }
}