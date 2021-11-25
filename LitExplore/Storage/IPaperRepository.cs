using System.Threading.Tasks;

namespace Core {
    public interface IPaperRepository {
        Task<PaperDetailsDTO> CreateAsync(PaperCreateDTO paper);
        Task<Option<PaperDetailsDTO>> ReadAsync(int paperId);
        Task<IReadOnlyCollection<PaperDTO>> ReadAsync();
        Task<Status> DeleteAsync(int paperId);
    }
}

