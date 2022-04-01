using Microsoft.AspNetCore.Mvc;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModelController : ControllerBase
{
    private readonly ILogger<ModelController> _logger;

    public ModelController(ILogger<ModelController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetModel")]
    public IEnumerable<IMeasurement> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index =>
                new Measurement(Guid.NewGuid(), DateTimeOffset.UtcNow, Random.Shared.Next(-20, 55), "Summary"))
            .ToArray();
    }
}
