namespace LitExplore.Storage;
public interface ITeamRepository {
    Task<TeamDTO> CreateAsync(TeamCreateDTO team, int creatorId);
    Task<Option<TeamDTO>> ReadAsync(int teamId);
    Task<IReadOnlyCollection<ConnectionDTO>> ReadConnectionsAsync();
    Task<Status> UpdateAsync(int id, TeamUpdateDTO team);
    Task<Status> DeleteAsync(int teamId);
}


