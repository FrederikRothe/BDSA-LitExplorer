namespace LitExplore.ApplicationLogic;
public class ConnectionRepository : IConnectionRepository
{

    private readonly ILitExploreContext _context;

    public ConnectionRepository(ILitExploreContext context)
    {
        _context = context;
    }

    public async Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection)
    {
        var entity = new Connection
        {
            Creator = connection.CreatorId == null? null : FindUser(connection.CreatorId),
            Paper1 = FindPaper(connection.PaperOneId),
            Paper1Id = connection.PaperOneId,
            Paper2 = FindPaper(connection.PaperTwoId),
            Paper2Id = connection.PaperTwoId,
            ConnectionType = connection.ConnectionType,
            Description = connection.Description == null? "" : connection.Description,
            Teams = new List<Team>()
        };

        _context.Connections.Add(entity);

        await _context.SaveChangesAsync();

        return new ConnectionDTO(
                            entity.Id,
                            (entity.Creator == null? null : entity.Creator.oid),
                            entity.Paper1.Id,
                            entity.Paper2.Id,
                            entity.ConnectionType,
                            entity.Description,
                            entity.Teams.Select(t => t.Id)
                         );
    }

    public async Task<Option<ConnectionDTO>> ReadAsync(int connectionId)
    {
        var connections = from c in _context.Connections
                          where c.Id == connectionId
                          select new ConnectionDTO(
                             c.Id,
                             (c.Creator == null? null : c.Creator.oid),
                             c.Paper1.Id,
                             c.Paper2.Id,
                             c.ConnectionType,
                             c.Description,
                             c.Teams.Select(t => t.Id)
                          );

        return await connections.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadPredefinedAsync()
        => (await _context.Connections
                          .Where(c => c.Creator == null)
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                (c.Creator == null? null : c.Creator.oid),
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                null, null))
                          .ToListAsync())
                          .AsReadOnly();

    public async Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(int connectionId) 
        => (await _context.Teams
                          .Where(t => t.Connections.Contains(FindConnection(connectionId)))
                          .Select(t => new TeamDTO(
                                t.Id,
                                t.TeamName,
                                t.Colour,
                                null, null, null))
                          .ToListAsync())
                          .AsReadOnly();
    

    public async Task<Status> UpdateAsync(int connectionId, ConnectionUpdateDTO connection)
    {
        var entity = FindConnection(connectionId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Id = connection.Id;
        entity.Paper1 = FindPaper(connection.PaperOneId);
        entity.Paper2 = FindPaper(connection.PaperTwoId);
        entity.ConnectionType = connection.ConnectionType;
        entity.Description = connection.Description == null? "" : connection.Description;

        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> DeleteAsync(int connectionId)
    {
        var entity = FindConnection(connectionId);

        if (entity == null)
        {
            return NotFound;
        }

        _context.Connections.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    private Paper FindPaper(int id) => _context.Papers.Where(p => p.Id == id).First();
    private Connection FindConnection(int id) => _context.Connections.Where(c => c.Id == id).First();
    private User FindUser(string userId) => _context.Users.Where(u => u.oid.Equals(userId)).First();

}
