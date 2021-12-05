namespace LitExplore.Storage;
public interface ITeamRepository {
    Task<TeamDTO> CreateAsync(TeamCreateDTO team, string creatorId);
    Task<Option<TeamDTO>> ReadAsync(int teamId);
    Task<IReadOnlyCollection<TeamDTO>> ReadUserTeamsAsync(string userId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync();
    Task<Status> UpdateAsync(int id, TeamUpdateDTO team);
    Task<Status> DeleteAsync(int teamId);
}


