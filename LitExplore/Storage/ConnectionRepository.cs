namespace LitExplore.Storage;
public class ConnectionRepository : IConnectionRepository
{

    private readonly ILitExploreContext _context;

    public ConnectionRepository(ILitExploreContext context)
    {
        _context = context;
    }

    public Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection)
    {
        var entity = new Connection
        {
            Paper1 = connection.Paper1,
            Paper2 = connection.Paper2,
            ConnectionTypes = connection.connectionTypes,
            Description = connection.description
        };

        _context.Connections.Add(entity);

        await _context.SaveChangesAsync();

        return new ConnectionDTO(
                            entity.Id,
                            entity.Paper1,
                            entity.Paper2,
                            entity.ConnectionTypes,
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

    public Task<Option<ConnectionDTO>> ReadAsync(int connectionId)
    {
        var connections = from c in _context.Connections
                         where c.Id == connectionId
                         select new ConnectionDTO(
                            c.Id,
                            c.PaperOneId,
                            c.PaperTwoId,
                            c.ConnectionType,
                            c.Description
                         );

        return await connections.FirstOrDefaultAsync();
    }

    public Task<IReadOnlyCollection<ConnectionDTO>> ReadTeamAsync(int teamId)
    {
        (await _context.Connections
                       .Select(c => new ConnectionDTO(c.Id, c.GivenName, c.Surname, c.AlterEgo))
                       .ToListAsync())
                       .AsReadOnly();
    }

    public Task<IReadOnlyCollection<ConnectionDTO>> ReadUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection)
    {
        throw new NotImplementedException();
    }
}
