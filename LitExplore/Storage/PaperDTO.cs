using System.ComponentModel.DataAnnotations;

namespace Storage
{
    public record PaperCreateDTO {
        [Required] 
        public string Document { get; init; }
        [Required] 
        public ICollection<string> Authors {get; init;}
        [Required] 
        public string Title { get; init; }
        [Required] 
        int Year
        [Required] 
        int Month
        [Required] 
        int Day
        }
    public record PaperDTO(int Id, [Required] string Document, [Required] ICollection<string> Authors, [Required] string Title, [Required] int Year, [Required] int Month, [Required] int Day) : PaperCreateDTO(Document, Authors, Title, Year, Month, Day);
} 