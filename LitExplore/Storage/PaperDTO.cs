namespace LitExplore.Storage;

public record PaperCreateDTO
{
    [Required]
    public string Document { get; init; }
    [Required]
    public ICollection<int> AuthorIDs { get; init; }
    [Required]
    [StringLength(50)]
    public string Title { get; init; }
    [Required]
    public int Year { get; init; }
    [Required]
    public int Month { get; init; }
    [Required]
    public int Day { get; init; }

    [Required]
    public ICollection<int> TagIDs { get; init; }
}
public record PaperDTO(int Id, [Required] string Document, IEnumerable<int> Authors, string Title, int Year, int Month, int Day, IEnumerable<int> TagIDs);

