using System.Collections.Concurrent;

namespace ${{ values.namespacePrefix }}.Infrastructure.InMemory;

public class InMemoryMeasurementRepository : IMeasurementRepository
{
    private readonly ConcurrentDictionary<Guid, IMeasurement> _storage = new();

    public InMemoryMeasurementRepository() { }

    public Task<IMeasurement> CreateAsync(IMeasurement measurement, CancellationToken cancellationToken)
    {
        _storage.AddOrUpdate(measurement.Id, (_) => measurement, (_, item) => item);
        return Task.FromResult(measurement);
    }

    public Task<IMeasurement?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _storage.TryGetValue(id, out var measurement);
        return Task.FromResult(measurement);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_storage.Remove(id, out var _));
    }
}