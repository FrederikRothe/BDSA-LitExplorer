namespace Storage {

    public class TeamRepsitory : ITeamRepository
    {
        public Task<TeamDTO> CreateAsync(TeamCreateDTO team)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<Option<TeamDTO>> ReadAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TeamDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(int id, TeamUpdateDTO team)
        {
            throw new NotImplementedException();
        }
    }
}