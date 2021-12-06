namespace LitExplore.Storage;
public record TeamCreateDTO
{
    [Required]
    public int TeamLeaderId { get; init; }

    [Required]
    [StringLength(50)]
    public string TeamName { get; init; }

    [Required]
    public int Colour { get; init; }
    
}

public record TeamDTO(int Id, string TeamName, int Colour, int? TeamLeaderId, IEnumerable<int>? UserIDs, IEnumerable<int>? ConnectionIDs);

public record TeamUpdateDTO : TeamCreateDTO
{
    public int Id { get; set; }
    public IEnumerable<int> UserIDs { get; init; }
    public IEnumerable<int> ConnectionIDs { get; init; }
};