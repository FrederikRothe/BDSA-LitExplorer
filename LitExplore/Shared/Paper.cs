namespace LitExplore.Shared;

public class Paper
{
    public ICollection<string> Authors { get; set; }

    public string Title { get; set; }

    public DateTime PublishDate { get; set; }

    public ICollection<string>? Tags { get; set; }

    public string Document { get; set; }

    public Paper(string document, ICollection<string> authors, string title, int year, int month, int day)
    {
        Document = document;
        Authors = authors;
        Title = title;
        PublishDate = new DateTime(year, month, day);
    }

    public override string ToString() => $"{Title}\n{PublishDate.ToString()}\n\n{Document}";
}