using System.ComponentModel.DataAnnotations;

namespace Storage

{   
    public record TeamDTO(int Id, string TeamLeaderId, string TeamName) : TeamCreateDTO(TeamLeaderId, TeamName);
    public record TeamCreateDTO
    {
        [Required] 
        public string TeamLeaderId { get; init; }
        
        [Required]
        [StringLength(50)]
        public string TeamName { get; init; }
    }
} 