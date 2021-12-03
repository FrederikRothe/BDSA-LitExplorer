namespace LitExplore.Storage;

public record ConnectionCreateDTO
{
    [Required]
    public int PaperOneId { get; init; }
    [Required]
    public int PaperTwoId { get; init; }
    [Required]
    public string? ConnectionType { get; init; }
    public string? Description { get; init; }
    [Required]
    public IEnumerable<int> TeamIDs { get; init; }
}

public record ConnectionDTO(int Id, int PaperOneId, int PaperTwoId, string? ConnectionType, string? Description, IEnumerable<int> TeamIDs);

public record ConnectionUpdateDTO : ConnectionCreateDTO
{
    public int Id { get; set; }
}
