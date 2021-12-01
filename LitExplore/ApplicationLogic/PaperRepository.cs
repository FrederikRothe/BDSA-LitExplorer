namespace LitExplore.Storage;
public class PaperRepository : IPaperRepository
{
    public Task<PaperDTO> CreateAsync(PaperCreateDTO paper)
    {
        throw new NotImplementedException();
    }

    public Task<Status> DeleteAsync(int paperId)
    {
        throw new NotImplementedException();
    }

    public Task<Option<PaperDTO>> ReadAsync(int paperId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<PaperDTO>> ReadAsync()
    {
        throw new NotImplementedException();
    }
}

