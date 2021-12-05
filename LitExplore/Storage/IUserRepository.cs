namespace LitExplore.Storage;
public interface IUserRepository {
    Task<UserDTO> CreateAsync(UserCreateDTO user);
    Task<Status> DeleteAsync(string userId);
    Task<Option<UserDTO>> ReadAsync(string userId);
}