namespace LitExplore.DomainServices;

public record ConnectionCreateDTO
{
    [Required]
    public string? CreatorId { get; set; }
    [Required]
    public int PaperOneId { get; set; }
    [Required]
    public int PaperTwoId { get; set; }
    [Required]
    public string ConnectionType { get; set; } = null!;
    public string? Description { get; set; }

    public int? TeamId { get; set; }
    
}

public record ConnectionDTO(int Id, string? CreatorId, int PaperOneId, int PaperTwoId, string ConnectionType, string? Description, IEnumerable<int>? TeamIDs);

public record ConnectionUpdateDTO : ConnectionCreateDTO
{
    public int Id { get; set; }
    public IEnumerable<int> TeamIDs { get; set; } = new List<int>();
}
