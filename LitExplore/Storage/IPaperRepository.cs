namespace LitExplore.Storage;
public interface IPaperRepository {
    Task<Option<PaperDTO>> ReadAsync(int paperId);
    Task<IReadOnlyCollection<PaperDTO>> ReadAsync();
}


