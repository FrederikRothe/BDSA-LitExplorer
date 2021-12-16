namespace LitExplore.DomainServices;

public interface IUserRepository 
{
    Task<UserDTO> CreateAsync(UserCreateDTO user);

    Task<Option<UserDTO>> ReadAsync(string userId);

    Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync(string userId);

    Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(string userId);
}