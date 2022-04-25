namespace ${{ values.namespacePrefix }}.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class Controller : ControllerBase
{
    private readonly IMeasurementRepository _repository;
    private readonly IMediator _mediator;
    private readonly ILogger<Controller> _logger;

    public Controller(IMediator mediator,
        IMeasurementRepository repository,
        ILogger<Controller> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<Measurement>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.FindAsync(id, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Measurement>> Add(Guid? guid, double temperatureC, string summary, CancellationToken cancellationToken)
    {
        var cmd = new CreateMeasurementCommand();
        var result = await _mediator.Send(cmd, cancellationToken);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid? guid)
    {
        throw new NotImplementedException();
    }
}