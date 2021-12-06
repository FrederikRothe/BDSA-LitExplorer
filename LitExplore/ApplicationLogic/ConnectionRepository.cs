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
                          .Where(c => !c.ConnectionType.Contains("other"))
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                c.Description,
                                null))
                          .ToListAsync())
                          .AsReadOnly();

    public async Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(int connectionId)
        => (await FindConnection(connectionId).Teams
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
        entity.Description = connection.Description;

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
    private User FindUser(string userId) => _context.Users.Where(u => u.Id.Equals(userId)).First();

}
