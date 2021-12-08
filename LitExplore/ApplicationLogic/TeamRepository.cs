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
                            entity.TeamLeader.Id,
                            entity.Users.Select(u => u.Id),
                            entity.Connections.Select(c => c.Id)
                        );
    }

    public async Task<Option<TeamDTO>> ReadAsync(int teamId)
    {
        var teams = from t in _context.Teams
                         where t.Id == teamId
                         select new TeamDTO(
                            t.Id,
                            t.TeamName,
                            t.Colour,
                            t.TeamLeader.Id,
                            t.Users.Select(u => u.Id),
                            t.Connections.Select(c => c.Id)
                         );

        return await teams.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(int teamId)
        => (await _context.Connections
                          .Where(c => c.Teams.Contains(FindTeam(teamId)))
                          .Select(c => new ConnectionDTO(
                                c.Id,
                                null,
                                c.Paper1.Id,
                                c.Paper2.Id,
                                c.ConnectionType,
                                c.Description,
                                null))
                          .ToListAsync())
                          .AsReadOnly();
    
    public async Task<IReadOnlyCollection<UserDTO>> ReadUsersAsync(int teamId)
        => (await _context.Users
                          .Where(u => (u.Teams.Contains(FindTeam(teamId)) || u.IsLeaderOf.Contains(FindTeam(teamId))))
                          .Select(u => new UserDTO(
                                u.Id,
                                null,
                                u.Name,
                                null, null))
                          .ToListAsync())
                          .AsReadOnly();

    public async Task<Status> UpdateAsync(int teamId, TeamUpdateDTO team)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Id = team.Id;
        entity.TeamLeader = _context.Users.Where(u => u.oid == team.TeamLeaderId).Single();
        entity.TeamName = team.TeamName;

        await _context.SaveChangesAsync();

        return Updated;
    }

    public async Task<Status> DeleteAsync(int teamId)
    {
        var entity = FindTeam(teamId);

        if (entity == null)
        {
            return NotFound;
        }

        _context.Teams.Remove(entity);
        await _context.SaveChangesAsync();

        return Deleted;
    }

    private Team FindTeam(int teamId) => _context.Teams.Where(t => t.Id == teamId).First();
}