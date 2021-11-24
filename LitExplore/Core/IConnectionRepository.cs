namespace LitExplorer.Core;

public interface IConnectionExplorer {
    Task<ConnectionDetailsDTO> CreateAsync(ConnectionCreateDTO connection);
    Task<Option<ConnectionDetailsDTO>> ReadAsync(int connectionId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadAsync();
    Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection);
    Task<Status> DeleteAsync(int connectionId);
}