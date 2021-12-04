
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[Route("api/[controller]")]
public class TeamManager : ControllerBase
{
    private readonly ILogger<PaperManager> _logger;
    private readonly ITeamRepository _repository;

    public TeamManager(ILogger<PaperManager> logger, ITeamRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(TeamDTO), 200)]
    [ProducesResponseType(401)]
    [HttpGet{id]}]
    public async Task<ActionResult<TeamDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(TeamDTO), 201)]
    [ProducesResponseType(401)]
    [HttpPost]
    public async Task<ActionResult<TeamDTO>> Post(TeamCreateDTO tdto, int creatorId) => await _repository.CreateAsync(tdto, creatorId);

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpDelete{id}]
    public async Task<IActionResult> Delete(int id) => (await _repository.DeleteAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [HttpPut{id}]
    public async Task<IActionResult> Put(int id, TeamUpdateDTO tcdto) => (await _repository.UpdateAsync(id, tcdto)).ToActionResult();
}