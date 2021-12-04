
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("api/[controller]")]
public class ConnectionManager : ControllerBase
{
    private readonly ILogger<PaperManager> _logger;
    private readonly IConnectionRepository _repository;

    public ConnectionManager(ILogger<PaperManager> logger, IConnectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 200)]
    [HttpGet{id]}]
    public async Task<ActionResult<ConnectionDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ConnectionDTO>), 200)]
    [HttpGet]
    public async Task<Option<IReadOnlyCollection<ConnectionDTO>>> GetConnections() => await _repository.ReadPredefinedAsync();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 201)]
    [HttpPost]
    public async Task<Option<ConnectionDTO>> Post(ConnectionCreateDTO ccdto) => await _repository.CreateAsync(ccdto);
    
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [HttpPut{id}]
    public async Task<IActionResult> Put(int id, ConnectionUpdateDTO ccdto) => (await _repository.UpdateAsync(id, ccdto)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [HttpDelete{id}]
    public async Task<IActionResult> Delete(int id) => (await _repository.DeleteAsync(id)).ToActionResult();
}