namespace LitExplore.ApplicationLogic;
public class PaperRepository : IPaperRepository
{
     private readonly ILitExploreContext _context;

    public PaperRepository(ILitExploreContext context)
    {
        _context = context;
    }

    
    public async Task<PaperDTO> CreateAsync(PaperCreateDTO paper)
    {
            var conflict =
                await _context.Papers
                              .Where(p => p.Title == paper.Title)
                              .Select(p => new PaperDTO(p.Id, p.Document, FindAuthorsId(p.Authors), p.Title, p.Date.Year, p.Date.Month, p.Date.Day))
                              .FirstOrDefaultAsync();

            if (conflict != null)
            {
                return (Conflict, conflict);
            }

            var entity = new Paper
            {
                Authors = FindAuthorsObj(paper.Authors),
                Title = paper.Title,
                Date = new DateTime(paper.Year, paper.Month, paper.Day),
                Document = paper.Document
            };

            _context.Papers.Add(entity);

            await _context.SaveChangesAsync();

            return new PaperDTO(entity.Id, entity.Document, FindAuthorsId(entity.Authors), entity.Title, entity.Date.Year, entity.Date.Month, entity.Date.Day);
        }

    public async Task<Status> DeleteAsync(int paperId)
    {
            var entity =
                await _context.Papers
                              .Include(c => c.Connections)
                              .FirstOrDefaultAsync(c => c.PaperOneId == paperId || c.PaperTwoId == paperId);

            if (entity == null)
            {
                return NotFound;
            }

            if (entity.Connections.Any())
            {
                return Conflict;
            }

            _context.Papers.Remove(entity);
            await _context.SaveChangesAsync();

            return Deleted;
        }

    public async Task<Option<PaperDTO>> ReadAsync(int paperId)
    {
        var papers = from p in _context.Papers
                    where p.Id == paperId
                    select new PaperDTO(p.Id, p.Document, FindAuthorsId(p.Authors), p.Title, p.Date.Year, p.Date.Month, p.Date.Day);
                
        return await papers.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<PaperDTO>> ReadAsync() =>
            (await _context.Papers
                           .Select(p => new PaperDTO(p.Id, p.Document, FindAuthorsId(p.Authors), p.Title, p.Date.Year, p.Date.Month, p.Date.Day))
                           .ToListAsync())
                           .AsReadOnly();
    
    private IEnumerable<Author> FindAuthorsObj(ICollection<int> ids){
        foreach (int id in ids)
        {
            yield return _context.Authors.Where(a => a.Id == id).First();
        }
    }

    private IEnumerable<int> FindAuthorsId(IEnumerable<Author> authors){
        foreach (Author author in authors)
        {
            yield return _context.Authors.Where(a => a.Equals(author)).Select(a => a.Id).First();
        }
    }
}

