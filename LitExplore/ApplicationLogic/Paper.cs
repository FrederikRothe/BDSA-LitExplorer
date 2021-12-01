namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }
    public ICollection<string> Authors { get; set; }

    public string Title { get; set; }

    public DateTime PublishDate { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();

    public string Document { get; set; }

    public Paper(string document, ICollection<string> authors, string title, int year, int month, int day, ICollection<string>? tags)
    {
        Document = document;
        Authors = authors;
        Title = title;
        PublishDate = new DateTime(year, month, day);
        if (tags != null) Tags = tags;
    }

    public override string ToString() => $"{Title}\n{PublishDate.ToString()}\n\n{Document}";
}
