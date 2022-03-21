using Microsoft.AspNetCore.Mvc;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModelController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ModelController> _logger;

    public ModelController(ILogger<ModelController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetModel")]
    public IEnumerable<IModel> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Model
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
