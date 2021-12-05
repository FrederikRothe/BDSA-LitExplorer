using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("api/[controller]")]
public class PaperManager : ControllerBase
{
    private readonly ILogger<PaperManager> _logger;
    private readonly IPaperRepository _repository;

    public PaperManager(ILogger<PaperManager> logger, IPaperRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(PaperDTO), 200)]
    [ProducesResponseType(401)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Option<PaperDTO>>> Get(int id) => await _repository.ReadAsync(id);

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ICollection<PaperDTO>), 200)]
    [ProducesResponseType(401)]
    [HttpGet]
    public async Task<IReadOnlyCollection<PaperDTO>> GetPapers() => await _repository.ReadAsync();
}