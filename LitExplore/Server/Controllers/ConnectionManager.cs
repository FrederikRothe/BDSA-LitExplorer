
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
    [HttpGet("{id}")]
    public async Task<ActionResult<ConnectionDTO>> Get(int connectionID) => await _repository.ReadAsync(connectionID);

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ICollection<ConnectionDTO>), 200)]
    [HttpGet("{teamId}")]
    public async Task<ActionResult<ICollection<ConnectionDTO>>> GetTeamConnections(int teamId) => await _repository.ReadTeamAsync(teamId);

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 201)]
    public async Task<IActionResult> Post(ConnectionCreateDTO ccdto) => await _repository.CreateAsync(ccdto);
    
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Put(int connectionID, ConnectionCreateDTO ccdto) => await _repository.UpdateAsync(connectionID, ccdto);

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Delete(int connectionID) => await _repository.DeleteAsync(connectionID);
}