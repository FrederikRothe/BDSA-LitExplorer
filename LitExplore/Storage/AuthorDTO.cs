namespace LitExplore.Storage;
   
public record AuthorDTO(int Id, string Name, ICollection<int> Papers);
public record AuthorCreateDTO
{
    [Required] 
    public string Name { get; init; }
    
    public ICollection<int>? Papers { get; init; }
}
public record AuthorUpdateDTO : AuthorCreateDTO 
{ 
    public int Id { get; set; }
};