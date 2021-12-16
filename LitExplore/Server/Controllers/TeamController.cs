using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    
    private readonly ITeamRepository _repository;

    public TeamController(ILogger<TeamController> logger, ITeamRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(TeamDTO), 201)]
    public async Task<IActionResult> Post(TeamCreateDTO tdto) 
    {
        var created = await _repository.CreateAsync(tdto);
        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(TeamDTO), 200)]
    [ProducesResponseType(401)]
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ConnectionDTO>), 200)]
    [ProducesResponseType(401)]
    [HttpGet("connections/{id}")]
    public async Task<IReadOnlyCollection<ConnectionDTO>> GetTeamConnections(int id) => await _repository.ReadConnectionsAsync(id);

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<UserDTO>), 200)]
    [ProducesResponseType(401)]
    [HttpGet("users/{id}")]
    public async Task<IReadOnlyCollection<UserDTO>> GetTeamUsers(int id) => await _repository.ReadUsersAsync(id);

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TeamUpdateDTO tcdto) => (await _repository.UpdateAsync(id, tcdto)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpPut("{id}/user/{userOid}")]
    public async Task<IActionResult> AddUser(int id, string userOid) => (await _repository.AddUserToTeamAsync(id, userOid)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpPut("{id}/connection/{connectionId}")]
    public async Task<IActionResult> ShareConnection(int id, int connectionId) => (await _repository.ShareConnectionAsync(id, connectionId)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => (await _repository.DeleteAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpDelete("{id}/user/{userOid}")]
    public async Task<IActionResult> RemoveUser(int id, string userOid) => (await _repository.RemoveUserAsync(id, userOid)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpDelete("{id}/connection/{connectionId}")]
    public async Task<IActionResult> RemoveConnection(int id, int connectionId) => (await _repository.RemoveConnectionAsync(id, connectionId)).ToActionResult();
}