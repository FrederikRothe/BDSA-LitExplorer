namespace LitExplore.ApplicationLogic;

public class Tag : IEquatable<Tag>
{
    public int Id {get; set;}
    
    [Required]
    public string Name {get; set;}

    public bool Equals(Tag that) => Name == that.Name ? true : false;
}