namespace LitExplore.Storage;
public interface ITeamRepository {
    Task<TeamDTO> CreateAsync(TeamCreateDTO team, string creatorId);
    Task<Option<TeamDTO>> ReadAsync(int teamId);
    Task<IReadOnlyCollection<TeamDTO>> ReadUserTeamsAsync(string userId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(int teamId);
    Task<IReadOnlyCollection<UserDTO>> ReadUsersAsync(int teamId)
    Task<Status> UpdateAsync(int teamId, TeamUpdateDTO team);
    Task<Status> DeleteAsync(int teamId);
}