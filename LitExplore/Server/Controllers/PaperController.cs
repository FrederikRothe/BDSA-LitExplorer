using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

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
    public async Task<ActionResult<Option<PaperDTO>>> Get(int id) => await _repository.ReadAsync(id);

    [AllowAnonymous]
    [HttpGet]
    public async Task<IReadOnlyCollection<PaperDTO>> GetPapers() => await _repository.ReadAsync();
}