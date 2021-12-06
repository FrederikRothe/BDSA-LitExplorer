namespace LitExplore.Storage;
public interface IUserRepository {
    Task<UserDTO> CreateAsync(UserCreateDTO user);
    Task<Option<UserDTO>> ReadAsync(string userId);
    Task<IEnumerable<ConnectionDTO>> ReadConnectionsAsync(string userId);
    Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(string userId);
    Task<Status> DeleteAsync(string userId);
}