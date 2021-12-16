namespace LitExplore.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PaperController : ControllerBase
{
    private readonly ILogger<PaperController> _logger;
    
    private readonly IPaperRepository _repository;

    public PaperController(ILogger<PaperController> logger, IPaperRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [AllowAnonymous]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(PaperDTO), 200)]
    [ProducesResponseType(401)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PaperDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<PaperDTO>> GetPapers() => await _repository.ReadAsync();
}