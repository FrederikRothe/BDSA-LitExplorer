namespace LitExplore.ApplicationLogic;

public class Paper
{
    public int Id { get; set; }
    public ICollection<string> Authors { get; set; }

    public string Title { get; set; }

    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }

    public ICollection<string>? Tags { get; set; }

    public string Document { get; set; }

    public Paper(string document, ICollection<string> authors, string title, int year, int month, int day)
    {
        Document = document;
        Authors = authors;
        Title = title;
        Year = year;
        Month = month;
        Day = day;
    }

    //public override string ToString() => $"{Title}\n{PublishDate.ToString()}\n\n{Document}";
}
