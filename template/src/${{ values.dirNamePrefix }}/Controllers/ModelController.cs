using MediatR;
using Microsoft.AspNetCore.Mvc;
using ${{ values.namespacePrefix }}.Domain;
using ${{ values.namespacePrefix }}.Commands;

namespace ${{ values.namespacePrefix }}.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModelController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ModelController> _logger;

    public ModelController(IMediator mediator,
        ILogger<ModelController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet(Name = "GetModel")]
    public IEnumerable<IMeasurement> Get(CancellationToken cancellationToken)
    {
        return Enumerable
            .Range(1, 5)
            .Select(index =>
                new Measurement(Guid.NewGuid(), DateTimeOffset.UtcNow, Random.Shared.Next(-20, 55), "Summary"))
            .ToArray();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Guid? guid, double temperatureC, string summary, CancellationToken cancellationToken)
    {
        var cmd = new CreateMeasurementCommand();
        var result = await _mediator.Send(cmd);

        return result ? Ok() : BadRequest();
    }
}
