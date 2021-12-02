
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ConnectionManager 
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
    public async Task<ActionResult<ConnectionDTO>> Get(int connectionID) => (await _repository.ReadAsync(connectionID)).ToActionResult();

    // [ProducesResponseType(404)]
    // [ProducesResponseType(401)]
    // [ProducesResponseType(typeof(IReadOnlyCollection<ConnectionDTO>), 200)]
    // public async Task<Option<IReadOnlyCollection<ConnectionDTO>>> GetConnections() => await _repository.ReadPredefinedAsync();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 201)]
    public async Task<Option<ConnectionDTO>> Post(ConnectionCreateDTO ccdto) => await _repository.CreateAsync(ccdto);
    
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Put(int connectionID, ConnectionUpdateDTO ccdto) => (await _repository.UpdateAsync(connectionID, ccdto)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete(int connectionID) => (await _repository.DeleteAsync(connectionID)).ToActionResult();
}