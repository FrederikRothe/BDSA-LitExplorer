namespace LitExplore.Storage;
   
public record TeamDTO(int Id, string TeamLeaderId, string TeamName);
public record TeamCreateDTO
{
    [Required] 
    public string TeamLeaderId { get; init; }
    
    [Required]
    [StringLength(50)]
    public string TeamName { get; init; }
}
public record TeamUpdateDTO : TeamCreateDTO 
{ 
    int Id { get; set; }
};