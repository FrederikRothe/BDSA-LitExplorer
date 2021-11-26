
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class TeamManager 

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
    public async Task<ActionResult<TeamDTO>> Get(int teamID) => await _repository.ReadAsync(teamID);

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(TeamDTO), 201)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<TeamDTO>> Post(TeamCreateDTO tdto) => await _repository.CreateAsync(tdto);

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Delete(int teamID) => await _repository.DeleteAsync(teamID);

    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> Put(int teamID, TeamCreateDTO tcdto) => await _repository.UpdateAsync(teamID, tcdto);

}