namespace ${{ values.namespacePrefix }}.Domain;

public interface IMeasurementRepository
{
    Task<Measurement> Add(Measurement measurement, CancellationToken cancellationToken);
    Task<Measurement?> FindAsync(Guid id, CancellationToken cancellationToken);
}
