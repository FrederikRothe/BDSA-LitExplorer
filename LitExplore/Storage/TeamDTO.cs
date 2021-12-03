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
    [Required]
    public ICollection<int> UserIDs { get; init; }
    [Required]
    public ICollection<int> ConnectionIDs { get; init; }
}
public record TeamDTO(int Id, int TeamLeaderId, string TeamName, int Colour, ICollection<int> UserIDs, ICollection<int> ConnectionIDs);
public record TeamUpdateDTO : TeamCreateDTO
{
    public int Id { get; set; }
};