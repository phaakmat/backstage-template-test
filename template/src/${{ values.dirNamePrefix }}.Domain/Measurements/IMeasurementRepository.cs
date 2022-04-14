namespace ${{ values.namespacePrefix }}.Domain;

public interface IMeasurementRepository
{
    Task<IMeasurement> AddAsync(Measurement measurement, CancellationToken cancellationToken);
    Task<IMeasurement?> FindAsync(Guid id, CancellationToken cancellationToken);
}
