namespace LitExplore.Storage;
public interface IPaperRepository {
    //Task<PaperDTO> CreateAsync(PaperCreateDTO paper);
    Task<Option<PaperDTO>> ReadAsync(int paperId);
    Task<IReadOnlyCollection<PaperDTO>> ReadAsync();
}


