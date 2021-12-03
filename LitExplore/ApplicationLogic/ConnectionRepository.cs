namespace LitExplore.ApplicationLogic;
public class ConnectionRepository : IConnectionRepository
{

    private readonly ILitExploreContext _context;

    public ConnectionRepository(ILitExploreContext context)
    {
        _context = context;
    }

    private Paper FindPaper(int id) => _context.Papers.Where(p => p.Id == id).First();

    public async Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection)
    {

        if (connection == null) return null;

        var paper1 = FindPaper(connection.PaperOneId);
        var paper2 = FindPaper(connection.PaperTwoId);
        var entity = new Connection
        {
            Paper1 = paper1,
            Paper2 = paper2,
            ConnectionType = connection.ConnectionType,
            Description = connection.Description
        };

        _context.Connections.Add(entity);

        await _context.SaveChangesAsync();

        return new ConnectionDTO(
                            entity.Id,
                            entity.Paper1.Id,
                            entity.Paper2.Id,
                            entity.ConnectionType,
                            entity.Description
                         );
    }

    public async Task<Status> DeleteAsync(int connectionId)
    {
        var entity = await _context.Connections.FindAsync(connectionId);

        if (entity == null)
        {
            return NotFound;
        }

        _context.Connections.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Option<ConnectionDTO>> ReadAsync(int connectionId)
    {
        var connections = from c in _context.Connections
                         where c.Id == connectionId
                         select new ConnectionDTO(
                            c.Id,
                            c.Paper1.Id,
                            c.Paper2.Id,
                            c.ConnectionType,
                            c.Description
                         );

        return await connections.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadPredefinedAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection)
    {
        var entity = await _context.Connections.FirstOrDefaultAsync(c => c.Id == id);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Id = connection.Id;
        entity.Paper1 = FindPaper(connection.PaperOneId);
        entity.Paper2 = FindPaper(connection.PaperTwoId);
        entity.ConnectionType = connection.ConnectionType;
        entity.Description = connection.Description;

        await _context.SaveChangesAsync();

        return Updated;
    }
}