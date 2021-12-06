namespace LitExplore.Storage;

public record AuthorCreateDTO
{
    [Required]
    public string Name { get; init; }
    [Required]
    public ICollection<int> PaperIDs { get; init; }
}

public record AuthorDTO(int Id, string? Name, ICollection<int>? PaperIDs);

public record AuthorUpdateDTO : AuthorCreateDTO
{
    public int Id { get; set; }
}
