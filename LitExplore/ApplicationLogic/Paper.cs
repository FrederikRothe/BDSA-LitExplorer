namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    [Required]
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public string Document { get; set; }
    public ICollection<Connection> AsPaper1 { get; set; } = new List<Connection>();
    public ICollection<Connection> AsPaper2 { get; set; } = new List<Connection>();
}
