namespace LitExplore.DomainServices;
public interface IPaperRepository {
    Task<Option<PaperDTO>> ReadAsync(int paperId);
    Task<IReadOnlyCollection<PaperDTO>> ReadAsync();
}


