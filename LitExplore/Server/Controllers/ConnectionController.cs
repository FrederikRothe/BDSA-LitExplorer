namespace LitExplore.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ConnectionController : ControllerBase
{
    private readonly ILogger<ConnectionController> _logger;
    private readonly IConnectionRepository _repository;

    public ConnectionController(ILogger<ConnectionController> logger, IConnectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    // Create
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 201)]
    [HttpPost]
    public async Task<Option<ConnectionDTO>> Post(ConnectionCreateDTO ccdto) => await _repository.CreateAsync(ccdto);
    
    // Read
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ConnectionDTO), 200)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ConnectionDTO>> Get(int id) => (await _repository.ReadAsync(id)).ToActionResult();

    [ProducesResponseType(typeof(IReadOnlyCollection<ConnectionDTO>), 200)]
    [HttpGet]
    public async Task<IReadOnlyCollection<ConnectionDTO>> GetAllConnections() => await _repository.ReadPredefinedAsync();

    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(IReadOnlyCollection<TeamDTO>), 200)]
    [HttpGet("teams/{id}")]
    public async Task<IReadOnlyCollection<TeamDTO>> GetTeams(int id) => await _repository.ReadTeamsAsync(id);

    // Update
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ConnectionUpdateDTO ccdto) => (await _repository.UpdateAsync(id, ccdto)).ToActionResult();

    // Delete
    [ProducesResponseType(404)]
    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => (await _repository.DeleteAsync(id)).ToActionResult();
}