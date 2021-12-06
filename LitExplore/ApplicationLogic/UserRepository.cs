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

    private User FindUser(string userId) => _context.Users.Where(u => u.Id.Equals(userId)).First();

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
}