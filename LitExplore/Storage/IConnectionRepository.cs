namespace LitExplore.Storage;
public interface IConnectionRepository {
    Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection);
    Task<Option<ConnectionDTO>> ReadAsync(int connectionId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadPredefinedAsync();
    Task<IReadOnlyCollection<TeamDTO>> ReadTeamsAsync(int connectionId);
    Task<Status> UpdateAsync(int connectionId, ConnectionUpdateDTO connection);
    Task<Status> DeleteAsync(int connectionId);
}


