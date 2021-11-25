namespace Storage {

    public class TeamRepsitory : ITeamRepository
    {
        public Task<TeamDetailsDTO> CreateAsync(TeamCreateDTO team)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int teamId)
        {
            throw new NotImplementedException();
        }

        public Task<Option<TeamDetailsDTO>> ReadAsync(int teamId)
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