namespace LitExplore.Storage;
   
public record TeamDTO(int Id, int TeamLeaderId, string TeamName);
public record TeamCreateDTO
{
    [Required] 
    public int TeamLeaderId { get; init; }
    
    [Required]
    [StringLength(50)]
    public string TeamName { get; init; }
}
public record TeamUpdateDTO : TeamCreateDTO 
{ 
    public int Id { get; set; }
};