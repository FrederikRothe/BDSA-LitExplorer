namespace LitExplore.Storage;
public record TeamCreateDTO
{
    [Required]
    public string TeamLeaderId { get; set; }

    [Required]
    [StringLength(50)]
    public string TeamName { get; set; }

    [Required]
    public int Colour { get; set; }
    
}

public record TeamDTO(int Id, string TeamName, int Colour, string? TeamLeaderId, IEnumerable<int>? UserIDs, IEnumerable<int>? ConnectionIDs);

public record TeamUpdateDTO : TeamCreateDTO
{
    public int Id { get; set; }
    public IEnumerable<int> UserIDs { get; init; }
    public IEnumerable<int> ConnectionIDs { get; init; }
};