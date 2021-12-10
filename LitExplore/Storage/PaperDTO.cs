namespace LitExplore.Storage;

public record PaperCreateDTO
{
    [Required]
    public string Document { get; init; } = null!;
    [Required]
    public IEnumerable<string> AuthorNames { get; init; } = new List<string>();
    [Required]
    [StringLength(50)]
    public string Title { get; init; } = null!;
    [Required]
    public int Year { get; init; }
    [Required]
    public int Month { get; init; }
    [Required]
    public int Day { get; init; }
    [Required]
    public IEnumerable<string> TagNames { get; init; } = new List<string>();
}
public record PaperDTO(int Id, string? Document, IEnumerable<string>? AuthorNames, string Title, int? Year, int? Month, int? Day, IEnumerable<string>? TagNames);

