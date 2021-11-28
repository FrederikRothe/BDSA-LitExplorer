namespace LitExplore.ApplicationLogic;

public class Tag : IEquatable<Tag>
{
    public int Id {get; set;}
    
    public string Name {get; set;}

    public Tag(string name)
    {
        Name = name;
    }

    public bool Equals(Tag that) => Name == that.Name ? true : false;
}