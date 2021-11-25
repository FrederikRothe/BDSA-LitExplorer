namespace Storage {
    public class ConnectionRepository : IConnectionRepository
    {
        public Task<ConnectionDetailsDTO> CreateAsync(ConnectionCreateDTO connection)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<Option<ConnectionDetailsDTO>> ReadAsync(int connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<ConnectionDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateAsync(int id, ConnectionUpdateDTO connection)
        {
            throw new NotImplementedException();
        }
    }
}