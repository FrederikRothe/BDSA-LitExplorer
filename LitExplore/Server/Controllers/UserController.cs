namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    
    private readonly IUserRepository _repository;

    public UserController(ILogger<UserController> logger, IUserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(UserDTO), 201)]
    [HttpPost]
    public async Task<IActionResult> Post(UserCreateDTO ucdto)
    {
        var created = await _repository.CreateAsync(ucdto);
        return CreatedAtAction(nameof(Get), new { created.Id }, created);
    }

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> Get(string id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(IReadOnlyCollection<ConnectionDTO>), 200)]
    [HttpGet("connections/{id}")]
    public async Task<IReadOnlyCollection<ConnectionDTO>> GetConnections(string id) => await _repository.ReadConnectionsAsync(id);

    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(IReadOnlyCollection<TeamDTO>), 200)]
    [ProducesResponseType(401)]
    [HttpGet("teams/{id}")]
    public async Task<IReadOnlyCollection<TeamDTO>> GetTeams(string id) => await _repository.ReadTeamsAsync(id);
}