namespace LitExplore.ApplicationLogic;

public class Tag : IEquatable<Tag>
{
    public int Id {get; set;}
    
    [Required]
    public string Name {get; set;}
    [Required]
    public ICollection<Paper>? Papers { get; set; }

    public bool Equals(Tag that) => Name == that.Name ? true : false;
}