namespace LitExplore.Storage;
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
                              .Select(c => new PaperDTO(p.id, p.Document, p.Authors, p.Title, p.Date.Year, p.Date.Month, p.Date.Day))
                              .FirstOrDefaultAsync();

            if (conflict != null)
            {
                return (Conflict, conflict);
            }

            var entity = new Paper
            {
                Authors = paper.Authors,
                Title = paper.Title,
                Date = 1,
                Document = paper.Document
            };

            _context.Papers.Add(entity);

            await _context.SaveChangesAsync();

            return (Created, new PaperDTO(entity.id, entity.Document, entity.Authors, entity.Title, entity.Date.Year, entity.Date.Month, entity.Date.Day));
        }

    public async Task<Status> DeleteAsync(int paperId)
    {
            var entity =
                await _context.Papers
                              .Include(c => c.Connections)
                              .FirstOrDefaultAsync(c => c.Id == paper1 || c.Id == paper2);

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
        var Paper = from p in _context.papers
                    where p.id == paperId
                    select new PaperDTO(p.id, p.Document, p.Authors, p.Title, p.Date.Year, p.Date.Month, p.Date.Day);
                
        return await _context.papers.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<PaperDTO>> ReadAsync() =>
            (await _context.Papers
                           .Select(c => new PaperDTO(p.id, p.Document, p.Authors, p.Title, p.Date.Year, p.Date.Month, p.Date.Day))
                           .ToListAsync())
                           .AsReadOnly();
}

