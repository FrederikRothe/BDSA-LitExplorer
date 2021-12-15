namespace LitExplore.Infrastructure;
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
                     select new PaperDTO(
                         p.Id, 
                         p.Document, 
                         p.Authors.Select(a => a.Name),
                         p.Title, 
                         p.Date.Year, 
                         p.Date.Month, 
                         p.Date.Day, 
                         p.Tags.Select(t => t.Name)
                     );
                
        return await papers.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<PaperDTO>> ReadAsync() =>
            (await _context.Papers
                           .Select(p => new PaperDTO(
                               p.Id, 
                               p.Document, 
                               p.Authors.Select(a => a.Name), 
                               p.Title, 
                               p.Date.Year, 
                               p.Date.Month, 
                               p.Date.Day, 
                               p.Tags.Select(t => t.Name)))
                           .ToListAsync())
                           .AsReadOnly();
}

