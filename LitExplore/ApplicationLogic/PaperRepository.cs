namespace LitExplore.ApplicationLogic;
public class PaperRepository : IPaperRepository
{
     private readonly ILitExploreContext _context;

    public PaperRepository(ILitExploreContext context)
    {
        _context = context;
    }

    public async Task<Option<PaperDTO>> ReadAsync(int paperId)
    {
        var papers = from p in _context.Papers
                     where p.Id == paperId
                     select new PaperDTO(p.Id, p.Document, FindAuthorsName(p.Authors), p.Title, p.Date.Year, p.Date.Month, p.Date.Day, FindTagsName(p.Tags));
                
        return await papers.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<PaperDTO>> ReadAsync() =>
            (await _context.Papers
                           .Select(p => new PaperDTO(
                               p.Id, 
                               p.Document, 
                               FindAuthorsName(p.Authors), 
                               p.Title, 
                               p.Date.Year, 
                               p.Date.Month, 
                               p.Date.Day, 
                               FindTagsName(p.Tags)))
                           .ToListAsync())
                           .AsReadOnly();

    private IEnumerable<string> FindAuthorsName(IEnumerable<Author> authors){
        foreach (Author author in authors)
        {
            yield return _context.Authors.Where(a => a.Equals(author)).Select(a => a.Name).First();
        }
    }

    private IEnumerable<string> FindTagsName(IEnumerable<Tag> tags){
        foreach (Tag tag in tags)
        {
            yield return _context.Authors.Where(t => t.Id == tag.Id).Select(a => a.Name).First();
        }
    }
}

