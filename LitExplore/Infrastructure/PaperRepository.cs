using Core;

namespace Infrastructure {
    public class PaperRepository : IPaperRepository
    {
        public Task<PaperDetailsDTO> CreateAsync(PaperCreateDTO paper)
        {
            throw new NotImplementedException();
        }

        public Task<Status> DeleteAsync(int paperId)
        {
            throw new NotImplementedException();
        }

        public Task<Option<PaperDetailsDTO>> ReadAsync(int paperId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<PaperDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
