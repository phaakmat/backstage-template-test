namespace ${{ values.namespacePrefix }}.Domain.Repositories;

public interface IMeasurementRepository
{
    Task<IMeasurement> CreateAsync(IMeasurement measurement, CancellationToken cancellationToken);
    Task<IMeasurement?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
