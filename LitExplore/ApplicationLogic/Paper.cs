namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }
    [Required]
    public ICollection<Author>? Authors { get; set; }
    [Required]
    [StringLength(50)]
    public string Title { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    [Required]
    public string Document { get; set; }
}
