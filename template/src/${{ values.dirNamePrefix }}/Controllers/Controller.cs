namespace ${{ values.namespacePrefix }}.Controllers;

[ApiController]
[ApiVersion("1.0")]
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


    [HttpGet("/api/v{version:apiVersion}/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Measurement>> FindById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.FindByIdAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost("/api/v{version:apiVersion}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Measurement>> Create([FromBody] CreateMeasurementCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("/api/v{version:apiVersion}/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteMeasurementCommand
        {
            MeasurementId = id
        };

        var found = await _mediator.Send(command, cancellationToken);

        return found ? Ok() : NoContent();
    }
}