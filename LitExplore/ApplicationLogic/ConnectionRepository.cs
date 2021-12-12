using System.IdentityModel.Tokens;
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
        var paper1 = FindPaper(connection.PaperOneId);
        var paper2 = FindPaper(connection.PaperTwoId);

        if (paper1 == null || paper2 == null) throw new Exception("Bad Request");

        var entity = new Connection
        {
            Creator = connection.CreatorId == null? null : FindUser(connection.CreatorId),
            Paper1 = paper1,
            Paper1Id = connection.PaperOneId,
            Paper2 = paper2,
            Paper2Id = connection.PaperTwoId,
            ConnectionType = connection.ConnectionType,
            Description = connection.Description == null? "" : connection.Description,
            Teams = new List<Team>()
        };

        if (connection.TeamId != null)
        {
            var team = FindTeam(connection.TeamId);
            if (team != null) entity.Teams.Add(team);
        }

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
                                c.Description, 
                                c.Teams.Select(t => t.Id)))
                          .ToListAsync())
                          .AsReadOnly();

    public async Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(int connectionId) 
    {
        var connection = FindConnection(connectionId);

        if (connection == null)
        {
            return new List<TeamDTO>();
        }

        var team = await _context.Teams
                          .Where(t => t.Connections.Contains(connection))
                          .Select(t => new TeamDTO(
                                t.Id,
                                t.TeamName,
                                t.Colour,
                                t.TeamLeader.oid, 
                                t.Users.Select(u => u.oid), 
                                t.Connections.Select(t => t.Id)))
                          .ToListAsync();
        return team.AsReadOnly();
    }

    public async Task<Status> UpdateAsync(int connectionId, ConnectionUpdateDTO connection)
    {
        var entity = FindConnection(connectionId);

        if (entity == null)
        {
            return NotFound;
        }

        var paper1 = FindPaper(connection.PaperOneId);
        var paper2 = FindPaper(connection.PaperTwoId);

        if (paper1 == null || paper2 == null)
        {
            return BadRequest;
        }

        entity.Id = connection.Id;
        entity.Paper1 = paper1;
        entity.Paper2 = paper2;
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

    private Paper? FindPaper(int id) => _context.Papers.Where(p => p.Id == id).FirstOrDefault();
    private Connection? FindConnection(int id) => _context.Connections.Where(c => c.Id == id).FirstOrDefault();
    private User? FindUser(string userId) => _context.Users.Where(u => u.oid.Equals(userId)).FirstOrDefault();
    private Team? FindTeam(int? id) => _context.Teams.Where(t => t.Id == id).FirstOrDefault();

}
