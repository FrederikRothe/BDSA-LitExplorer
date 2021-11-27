namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }
    public ICollection<Author> Authors { get; set; }

    public string Title { get; set; }

    public DateTime PublishDate { get; set; }

    public ICollection<string>? Tags { get; set; }

    public string Document { get; set; }

    public Paper(string document, ICollection<Author> authors, string title, int year, int month, int day, ICollection<string>? tags)
    {
        Document = document;
        Authors = authors;
        Title = title;
        PublishDate = new DateTime(year, month, day);
        Tags = tags;
    }
}
