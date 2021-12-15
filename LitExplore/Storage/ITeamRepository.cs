namespace LitExplore.Storage;
public interface ITeamRepository {
    Task<TeamDTO> CreateAsync(TeamCreateDTO team);
    Task<Option<TeamDTO>> ReadAsync(int teamId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(int teamId);
    Task<IReadOnlyCollection<UserDTO>> ReadUsersAsync(int teamId);
    Task<Status> UpdateAsync(int teamId, TeamUpdateDTO team);
    Task<Status> AddUserToTeamAsync(int teamId, string userOid);
    Task<Status> ShareConnectionAsync(int teamId, int connectionId);
    Task<Status> DeleteAsync(int teamId);
    Task<Status> RemoveConnectionAsync(int teamId, int connectionId);
    Task<Status> RemoveUserAsync(int teamId, string userOid);
}