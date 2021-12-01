namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }

    public ICollection<Author> Authors { get; set; }

    public string Title { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }
    
    public int Day { get; set; }

    public ICollection<Tag>? Tags { get; set; }

    public string Document { get; set; }

    public Paper(string document, ICollection<Author> authors, string title, int year, int month, int day, ICollection<Tag>? tags)
    {
        Document = document;
        Authors = authors;
        Title = title;
        Year = year;
        Month = month;
        Day = day;
        Tags = tags;
    }
}
