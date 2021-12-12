namespace LitExplore.ApplicationLogic;

public class TeamRepository : ITeamRepository
{

    private readonly ILitExploreContext _context;

    public TeamRepository(ILitExploreContext context)
    {
        _context = context;
    }

    public async Task<TeamDTO> CreateAsync(TeamCreateDTO team, string creatorId)
    {
        if (team == null) return null;
        var teamLeader = _context.Users.Where(u => u.oid == creatorId).SingleOrDefault();
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
         var matches = (from t in _context.Teams
                      where t.Id == teamId
                      select t.Id).Count();
        if(matches == 0) return null;

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
        var matches = (from t in _context.Teams
                      where t.Id == teamId
                      select t.Id).Count();
        if(matches == 0) return new List<ConnectionDTO>().AsReadOnly();

        var connections = (await _context.Connections
                          .Where(c => c.Teams.Contains(FindTeam(teamId)))
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                c.Creator.oid,
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                c.Description,
                                c.Teams.Select(t => t.Id)))
                          .ToListAsync())
                          .AsReadOnly();
        
        return connections;
    }
    public async Task<IReadOnlyCollection<UserDTO>> ReadUsersAsync(int teamId){
        var matches = (from t in _context.Teams
                      where t.Id == teamId
                      select t.Id).Count();
        if(matches == 0) return new List<UserDTO>().AsReadOnly();

        var users =  (await _context.Users
                            .Where(u => (u.Teams.Contains(FindTeam(teamId)) || u.IsLeaderOf.Contains(FindTeam(teamId))))
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
        var matches = (from t in _context.Teams
                      where t.Id == teamId
                      select t.Id).Count();
        if(matches == 0) return NotFound;
        
        var entity = FindTeam(teamId);

        entity.TeamLeader = _context.Users.Where(t => t.oid == team.TeamLeaderId).Single();
        entity.TeamName = team.TeamName;
        if (team.Colour > 0 && team.Colour < 5) entity.Colour = team.Colour;
        
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> AddUserToTeamAsync(int teamId, string userOid)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Users.Add(FindUser(userOid));
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> ShareConnectionAsync(int teamId, int connectionId)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Connections.Add(FindConnection(connectionId));
        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> DeleteAsync(int teamId)
    {
        var matches = (from t in _context.Teams
                      where t.Id == teamId
                      select t.Id).Count();
        if(matches == 0) return NotFound;
        
        var entity = FindTeam(teamId);

        _context.Teams.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Status> RemoveConnectionAsync(int teamId, int connectionId)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Connections.Remove(FindConnection(connectionId));
        await _context.SaveChangesAsync();

        return Deleted;
    }

    public async Task<Status> RemoveUserAsync(int teamId, string userOid)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Users.Remove(FindUser(userOid));
        await _context.SaveChangesAsync();

        return Deleted;
    }

    private Team FindTeam(int teamId) => _context.Teams.Where(t => t.Id == teamId).First();
    private User FindUser(string userOid) => _context.Users.Where(u => u.oid == userOid).First();
    private Connection FindConnection(int connectionId) => _context.Connections.Where(c => c.Id == connectionId).First();
}

