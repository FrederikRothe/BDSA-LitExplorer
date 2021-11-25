namespace Storage
{
    public record PaperCreateDTO([Required] string Document, [Required] ICollection<string> Authors, [Required] string Title, [Required] int Year, [Required] int Month, [Required] int Day);
    public record PaperDTO(int Id, [Required] string Document, [Required] ICollection<string> Authors, [Required] string Title, [Required] int Year, [Required] int Month, [Required] int Day) : PaperCreateDTO(Authors, Title, Year, Month, Day);
} 