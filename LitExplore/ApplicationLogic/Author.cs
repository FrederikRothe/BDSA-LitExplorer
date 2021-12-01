namespace LitExplore.ApplicationLogic;

public class Author : IEquatable<Author>
{
    public int Id {get; set;}
    
    public string Name {get; set;}

    public Author(string name)
    {
        Name = name;
    }

    public bool Equals(Author that) => Name == that.Name ? true : false;
}