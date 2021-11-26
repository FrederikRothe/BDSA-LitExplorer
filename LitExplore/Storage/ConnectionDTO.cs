using System.ComponentModel.DataAnnotations;

namespace Storage {
    public record ConnectionDTO(int Id, int PaperOneId, int PaperTwoId, string ConnectionType, string Description);

    public record ConnectionCreateDTO
    {
        [Required]
        public int PaperOneId {get; set;}
        [Required]
        public int PaperTwoId {get; set;}
        [Required]
        public string ConnectionType {get; set;}
        public string? Description {get; set;}
    }

    public record ConnectionUpdateDTO : ConnectionCreateDTO
    {
        public int Id {get; set;}
    }
}