using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("api/[controller]")]
public class PaperManager 

{
    private readonly ILogger<PaperManager> _logger;
    private readonly IPaperRepository _repository;

    public PaperManager(ILogger<PaperManager> logger, IPaperRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    /* [ProducesResponseType(404)]
    [ProducesResponseType(typeof(PaperDTO), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PaperDTO>> Get(int paperID) => await _repository.ReadAsync(paperID);

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(ICollection<PaperDTO>), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<PaperDTO>> GetPapers() => await _repository.ReadAllAsync();
 */

}