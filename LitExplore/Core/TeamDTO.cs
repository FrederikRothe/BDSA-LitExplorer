namespace LitExplore.Core

{
    public record TeamCreateDTO([Required] User TeamLeader, [Required] string TeamName);
    public record TeamDTO(int Id, [Required] User TeamLeader, [Required] string TeamName) : TeamCreateDTO(TeamLeader, TeamName);
}