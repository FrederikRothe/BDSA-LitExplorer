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
        if (user == null) throw new Exception("Bad Request");

        var entity = FindUserOid(user.oid);

        if (entity == null) 
        {
            entity = new User
            {
                oid = user.oid,
                Name = user.Name == null? "Unknown User" : user.Name,
                Connections = new List<Connection>(),
                Teams = new List<Team>()
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync();  
        }

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
                    where u.oid == userId
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
                          .Where(c => c.Creator != null && c.Creator.Equals(FindUserOid(userId)))
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

    public async Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(string userId)
    {
        var user = FindUserOid(userId);

        if (user == null)
        {
            return new List<TeamDTO>();
        }

        var teams = (await _context.Teams.Where(t => t.Users.Contains(user))
                                .Select(t => new TeamDTO(
                                        t.Id,
                                        t.TeamName,
                                        t.Colour,
                                        t.TeamLeader.oid, 
                                        t.Users.Select(u => u.oid), 
                                        t.Connections.Select(c => c.Id)))
                                .ToListAsync())
                                .AsReadOnly();
        return teams;
    }
    private User? FindUserOid(string userOid) => _context.Users.Where(u => u.oid == userOid).FirstOrDefault();
}