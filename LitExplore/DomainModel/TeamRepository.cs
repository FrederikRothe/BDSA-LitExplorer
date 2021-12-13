namespace LitExplore.DomainModel;

public class TeamRepository : ITeamRepository
{

    private readonly ILitExploreContext _context;

    public TeamRepository(ILitExploreContext context)
    {
        _context = context;
    }

    public async Task<TeamDTO> CreateAsync(TeamCreateDTO team)
    {
        var teamLeader = _context.Users.Where(u => u.oid == team.TeamLeaderId).Single();
        var entity = new Team
        {
            TeamLeader = teamLeader,
            TeamName = team.TeamName,
            Colour = team.Colour,
            Users = new List<User>() {teamLeader},
            Connections = new List<Connection>()
        };

        _context.Teams.Add(entity);

        await _context.SaveChangesAsync();

        return new TeamDTO(
                            entity.Id,
                            entity.TeamName,
                            entity.Colour,
                            entity.TeamLeader.oid,
                            entity.Users.Select(u => u.oid),
                            entity.Connections.Select(c => c.Id)
                        );
    }

    public async Task<Option<TeamDTO>> ReadAsync(int teamId)
    {
        var team = FindTeam(teamId);

        if (team == null) return null; 

        var teams = from t in _context.Teams
                         where t.Id == teamId
                         select new TeamDTO(
                            t.Id,
                            t.TeamName,
                            t.Colour,
                            t.TeamLeader.oid,
                            t.Users.Select(u => u.oid),
                            t.Connections.Select(c => c.Id)
                         );

        return await teams.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(int teamId)
    {
        var team = FindTeam(teamId);

        if (team == null) 
        {
            return new List<ConnectionDTO>().AsReadOnly();
        }

        var connections = (await _context.Connections
                          .Where(c => c.Teams.Contains(team))
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                c.Creator == null? null : c.Creator.oid,
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                c.Description,
                                c.Teams.Select(t => t.Id)))
                          .ToListAsync())
                          .AsReadOnly();

        return connections;
    }
    public async Task<IReadOnlyCollection<UserDTO>> ReadUsersAsync(int teamId)
    {
        var team = FindTeam(teamId);

        if (team == null) 
        {
            return new List<UserDTO>().AsReadOnly();
        }

        var users =  (await _context.Users
                            .Where(u => (u.Teams.Contains(team) || u.IsLeaderOf.Contains(team)))
                            .Select(u => new UserDTO(
                                    u.Id,
                                    u.oid,
                                    u.Name,
                                    u.Connections.Select(c => c.Id), 
                                    u.Teams.Select(t => t.Id)))
                            .ToListAsync())
                            .AsReadOnly();
        
        return users;            
    }
    public async Task<Status> UpdateAsync(int teamId, TeamUpdateDTO team)
    {        
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.TeamLeader = _context.Users.Where(t => t.oid == team.TeamLeaderId).Single();
        entity.TeamName = team.TeamName;
        if (team.Colour > 0 && team.Colour < 5) entity.Colour = team.Colour;
        
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> AddUserToTeamAsync(int teamId, string userOid)
    {
        var team = FindTeam(teamId);
        var user = FindUser(userOid);

        if (team == null || user == null)
        {
            return NotFound;
        }

        if (!team.Users.Contains(user)) team.Users.Add(user);
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> ShareConnectionAsync(int teamId, int connectionId)
    {
        var team = FindTeam(teamId);
        var connection = FindConnection(connectionId);

        if (team == null || connection == null)
        {
            return NotFound;
        }

        if (!team.Connections.Contains(connection)) team.Connections.Add(connection);
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> DeleteAsync(int teamId)
    {        
        var team = FindTeam(teamId);
        
        if (team == null)
        {
            return NotFound;
        }

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Status> RemoveConnectionAsync(int teamId, int connectionId)
    {
        var team = FindTeam(teamId);
        var connection = FindConnection(connectionId); 

        if (team == null || connection == null) 
        {
            return NotFound;
        }

        team.Connections.Remove(connection);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Status> RemoveUserAsync(int teamId, string userOid)
    {
        var team = FindTeam(teamId);
        var user = FindUser(userOid);

        if (team == null || user == null)
        {
            return NotFound;
        }

        team.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    private Team? FindTeam(int teamId) => _context.Teams.Include(t => t.Connections).Include(t => t.Users).Where(t => t.Id == teamId).FirstOrDefault();
    private User? FindUser(string userOid) => _context.Users.Where(u => u.oid == userOid).FirstOrDefault();
    private Connection? FindConnection(int connectionId) => _context.Connections.Where(c => c.Id == connectionId).FirstOrDefault();
}

