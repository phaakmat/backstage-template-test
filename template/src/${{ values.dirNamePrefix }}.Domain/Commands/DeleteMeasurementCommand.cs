namespace Funda.FeatureApiTemplate.Domain.Commands;
public class DeleteMeasurementCommand : IRequest<bool>
{
    public Guid MeasurementId { get; set; }
}
