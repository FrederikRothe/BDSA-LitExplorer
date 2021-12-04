namespace LitExplore.Storage;
public interface IUserRepository {
    Task<UserDTO> CreateAsync(UserCreateDTO user);
    Task<Status> DeleteAsync(int userId);
    Task<Option<UserDTO>> ReadAsync(int userId);
}


