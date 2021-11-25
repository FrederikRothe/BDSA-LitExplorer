namespace Storage

{
    public record TeamCreateDTO([Required] string TeamLeaderId, [Required] string TeamName);
    public record TeamDTO(int Id, [Required] string TeamLeaderId, [Required] string TeamName) : TeamCreateDTO(TeamLeaderId, TeamName);
} 