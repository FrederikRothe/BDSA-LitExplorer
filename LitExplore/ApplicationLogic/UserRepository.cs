namespace LitExplore.ApplicationLogic;

public class UserRepository : IUserRepository
{

    private readonly ILitExploreContext _context;

    public UserRepository(ILitExploreContext context)
    {
        _context = context;
    }
    public async Task<UserDTO> CreateAsync(UserCreateDTO user)
    {
        if (user == null) return null;
        var entity = new User
        {
            oid = user.oid,
            Name = user.Name,
            Connections = new List<Connection>(),
            Teams = new List<Team>()
        };

        _context.Users.Add(entity);

        await _context.SaveChangesAsync();

        return new UserDTO(
                            entity.Id,
                            entity.oid,
                            entity.Name,
                            entity.Connections.Select(c => c.Id),
                            entity.Teams.Select(t => t.Id)
                        );
    }

    public async Task<Option<UserDTO>> ReadAsync(string userId)
    {
        var users = from u in _context.Users
                    where u.Id.Equals(userId)
                    select new UserDTO(
                        u.Id,
                        u.oid,
                        u.Name,
                        u.Connections.Select(c => c.Id),
                        u.Teams.Select(t => t.Id)
                    );

        return await users.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(string userId)
        => (await _context.Connections
                          .Where(c => c.Creator.Equals(FindUserOid(userId)))
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                c.Description,
                                null))
                          .ToListAsync())
                          .AsReadOnly();

    public async Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(string userId)
        => (await _context.Teams.Where(t => t.Users.Contains(FindUserOid(userId)))
                                .Select(t => new TeamDTO(
                                        t.Id,
                                        t.TeamName,
                                        t.Colour,
                                        null, null, null))
                                .ToListAsync())
                                .AsReadOnly();
    
    public async Task<Status> DeleteAsync(string userId)
    {
        var entity = FindUser(userId);

        if (entity == null)
        {
            return NotFound;
        }

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }
    
    private User FindUser(string userId) => _context.Users.Where(u => u.Id.Equals(userId)).First();
    private User FindUserOid(string userOid) => _context.Users.Where(u => u.oid == userOid).First();
}