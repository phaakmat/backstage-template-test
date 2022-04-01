using MediatR;

namespace ${{ values.namespacePrefix }}.Commands;

public class CreateMeasurementCommand : IRequest<bool>
{
    public double TemperatureC { get; set; }
    public string? Summary { get; set; }

}
