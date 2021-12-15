namespace LitExplore.DomainServices;

public record UserCreateDTO
{
    [Required]
    public string oid { get; set; } = null!; 
    [Required]
    public string? Name { get; set; } = "";
}

public record UserDTO(int Id, string oid, string? Name, IEnumerable<int>? ConnectionIDs, IEnumerable<int>? TeamIDs);

public record UserUpdateDTO : UserCreateDTO {
    public int Id { get; set; }
    public IEnumerable<int>? ConnectionIDs { get; set; } = new List<int>();
    public IEnumerable<int>? TeamIDs { get; set; } = new List<int>();
}