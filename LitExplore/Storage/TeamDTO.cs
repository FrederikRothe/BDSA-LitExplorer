namespace LitExplore.Storage;
public record TeamCreateDTO
{
    [Required]
    public string TeamLeaderId { get; init; }

    [Required]
    [StringLength(50)]
    public string TeamName { get; init; }

    [Required]
    public int Colour { get; init; }
    
}
public record TeamDTO(int Id, string TeamLeaderId, string TeamName, int Colour, IEnumerable<string> UserIDs, IEnumerable<int> ConnectionIDs);
public record TeamUpdateDTO : TeamCreateDTO
{
    public int Id { get; set; }
    public IEnumerable<string> UserIDs { get; init; }
    public IEnumerable<int> ConnectionIDs { get; init; }
};