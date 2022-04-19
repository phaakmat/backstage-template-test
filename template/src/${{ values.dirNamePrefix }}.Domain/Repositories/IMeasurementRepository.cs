using ${{ values.namespacePrefix }}.Domain.Models;

namespace ${{ values.namespacePrefix }}.Domain.Repositories;

public interface IMeasurementRepository
{
    Task<IMeasurement> AddAsync(Measurement measurement, CancellationToken cancellationToken);
    Task<IMeasurement?> FindAsync(Guid id, CancellationToken cancellationToken);
}
