namespace LitExplore.Storage;
public record TeamCreateDTO
{
    [Required]
    public string TeamLeaderId { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string TeamName { get; set; } = null!;

    [Required]
    public int Colour { get; set; }
    
}

public record TeamDTO(int Id, string TeamName, int Colour, string? TeamLeaderId, IEnumerable<string>? UserIDs, IEnumerable<int>? ConnectionIDs);

public record TeamUpdateDTO : TeamCreateDTO
{
    public int Id { get; set; }
};