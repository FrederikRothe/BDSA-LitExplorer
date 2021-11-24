namespace LitExplorer.Core;

public interface ITeamRepository {
    Task<TeamDetailsDTO> CreateAsync(TeamCreateDTO team);
    Task<Option<TeamDetailsDTO>> ReadAsync(int teamId);
    Task<IReadOnlyCollection<TeamDTO>> ReadAsync();
    Task<Status> UpdateAsync(int id, TeamUpdateDTO team);
    Task<Status> DeleteAsync(int teamId);
}