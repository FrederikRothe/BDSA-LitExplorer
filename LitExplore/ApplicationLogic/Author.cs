namespace LitExplore.ApplicationLogic;

public class Author {
    public int Id { get; set; }
    
    [StringLength(25)]
    public string Name { get; set; } = "";
    public ICollection<Paper> Papers { get; set; } = new List<Paper>();

    public bool Equals(Author that) => Name == that.Name ? true : false;
}