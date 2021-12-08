namespace LitExplore.Storage;

public record TagCreateDTO
{
    [Required]
    public string Name { get; init; }
    [Required]
    public ICollection<int> PaperIDs { get; init; }
}

public record TagDTO(int Id, string? Name, ICollection<int>? PaperIDs);

public record TagUpdateDTO : TagCreateDTO
{
    public int Id { get; set; }
}

