namespace LitExplore.Storage;

public record UserCreateDTO
{
    [Required]
    public string oid { get; set; }    
    [Required]
    public string Name { get; set; }    
}

public record UserDTO(int id, string oid, string Name, IEnumerable<int> ConnectionIDs, IEnumerable<int> TeamIDs);

public record UserUpdateDTO : UserCreateDTO {
    public int Id { get; set; }
    public IEnumerable<int> ConnectionIDs { get; set; }
    public IEnumerable<int> TeamIDs { get; set; }
}