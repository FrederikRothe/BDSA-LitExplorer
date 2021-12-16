namespace LitExplore.DomainModel;

public class Tag : IEquatable<Tag>
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public ICollection<Paper> Papers { get; set; } = new List<Paper>();

    public bool Equals(Tag? that)
    {
        if (that == null) return false;
        return Name == that.Name;
    }
}