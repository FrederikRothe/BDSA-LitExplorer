namespace LitExplore.Core;

public record ConnectionDTO(int Id, Paper Paper1, Paper Paper2, string ConectionType, string Description);

public record ConnectionCreationDTO
{    
    [Required]
    public Paper Paper1 { get; set; }

    [Required]
    public Paper Paper2 { get; set; }

    [Required]
    public string ConnectionType { get; set; }

    public string? Description { get; set; }
}

public record ConnectionUpdateDTO : ConnectionCreationDTO 
{
    public int Id { get; set; }
}