
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserConnectionManager : ControllerBase
{
    private readonly ILogger<PaperManager> _logger;
    private readonly IConnectionRepository _repository;

    public UserConnectionManager(ILogger<PaperManager> logger, IConnectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(IEnumerable<ConnectionDTO>), 200)]
    [HttpGet("{id}")]
    public Task<IEnumerable<ConnectionDTO>> Get(string id) => _repository.ReadUserConnsAsync(id);

}