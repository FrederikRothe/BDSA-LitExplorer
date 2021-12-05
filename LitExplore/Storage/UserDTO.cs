namespace LitExplore.Storage;

public record UserCreateDTO
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }    
}

public record UserDTO(string Id, string Name, IEnumerable<int> ConnectionIDs, IEnumerable<int> TeamIDs);

public record UserUpdateDTO : UserCreateDTO {
    public IEnumerable<int> ConnectionIDs { get; set; }
    public IEnumerable<int> TeamIDs { get; set; }
}