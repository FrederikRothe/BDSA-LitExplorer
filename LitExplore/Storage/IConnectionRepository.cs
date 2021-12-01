namespace LitExplore.Storage;
public interface IConnectionRepository {
    Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection);
    Task<Option<ConnectionDTO>> ReadAsync(int connectionId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadTeamAsync(int teamId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadUserAsync(int userId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadAsync();
    Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection);
    Task<Status> DeleteAsync(int connectionId);
}


