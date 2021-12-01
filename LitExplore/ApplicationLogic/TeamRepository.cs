namespace LitExplore.ApplicationLogic;

public class TeamRepository : ITeamRepository
{

    private readonly ILitExploreContext _context;

    public TeamRepository(ILitExploreContext context)
    {
        _context = context;
    }
    public async Task<TeamDTO> CreateAsync(TeamCreateDTO team, int creatorId)
    {
        if (team == null) return null;
        var entity = new Team
        {
            TeamLeader = _context.Users.Where(u => u.Id == creatorId).SingleOrDefault(),
            TeamName = team.TeamName
        };

        _context.Teams.Add(entity);

        await _context.SaveChangesAsync();

        return new TeamDTO(
                            entity.Id,
                            entity.TeamLeader.Id,
                            entity.TeamName
                        );
    }

    private Team FindTeam(int teamId) => _context.Teams.Where(t => t.Id == teamId).First();

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

    public async Task<Option<TeamDTO>> ReadAsync(int teamId)
    {
        var teams = from t in _context.Teams
                         where t.Id == teamId
                         select new TeamDTO(
                            t.Id,
                            t.TeamLeader.Id,
                            t.TeamName
                         );

        return await teams.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Status> UpdateAsync(int id, TeamUpdateDTO team)
    {
        var entity = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

        if (entity == null)
        {
            return NotFound;
        }

        entity.Id = team.Id;
        entity.TeamLeader = _context.Users.Where(u => u.Id == team.TeamLeaderId).Single();
        entity.TeamName = team.TeamName;

        await _context.SaveChangesAsync();

        return Updated;
    }
}
