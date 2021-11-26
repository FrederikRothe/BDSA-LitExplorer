namespace Storage {
    public interface ITeamRepository {
        Task<TeamDTO> CreateAsync(TeamCreateDTO team);
        Task<Option<TeamDTO>> ReadAsync(int teamId);
        Task<IReadOnlyCollection<TeamDTO>> ReadAsync();
        Task<Status> UpdateAsync(int id, TeamUpdateDTO team);
        Task<Status> DeleteAsync(int teamId);
    }
}

