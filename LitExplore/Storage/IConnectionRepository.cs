namespace LitExplore.Storage;
public interface IConnectionRepository {
    Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection);
    Task<Option<ConnectionDTO>> ReadAsync(int connectionId);
    Task<IEnumerable<ConnectionDTO>> ReadUserConnsAsync(string userId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadPredefinedAsync();
    Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection);
    Task<Status> DeleteAsync(int connectionId);
}


