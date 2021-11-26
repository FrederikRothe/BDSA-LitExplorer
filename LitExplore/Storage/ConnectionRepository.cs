namespace Storage {
    public class ConnectionRepository : IConnectionRepository
    {
        public Task<ConnectionDTO> CreateAsync(ConnectionCreateDTO connection)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<Option<ConnectionDTO>> ReadAsync(int connectionId)
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