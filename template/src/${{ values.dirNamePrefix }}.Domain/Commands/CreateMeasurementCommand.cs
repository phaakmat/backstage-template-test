namespace ${{ values.namespacePrefix }}.Domain.Commands;

public class CreateMeasurementCommand : IRequest<IMeasurement>
{
    public Guid? MeasurementId { get; set; }
    public double TemperatureC { get; set; }
    public string? Summary { get; set; }
}
