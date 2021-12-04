
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserManager : ControllerBase
{
    private readonly ILogger<PaperManager> _logger;
    private readonly IUserRepository _repository;

    public ConnectionManager(ILogger<PaperManager> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [HttpGet{id]}]
    public async Task<ActionResult<UserDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(UserDTO), 201)]
    [HttpPost]
    public async Task<Option<UserDTO>> Post(UserCreateDTO ucdto) => await _repository.CreateAsync(ucdto);
}