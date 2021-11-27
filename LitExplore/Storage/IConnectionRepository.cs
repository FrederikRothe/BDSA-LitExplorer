namespace LitExplore.Storage;
public interface IConnectionRepository {
    Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection);
    Task<Option<ConnectionDTO>> ReadAsync(int connectionId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadAsync();
    Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection);
    Task<Status> DeleteAsync(int connectionId);
}


