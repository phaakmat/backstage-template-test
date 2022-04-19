using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain.Repositories;
using ${{ values.namespacePrefix }}.Domain.Models;

namespace ${{ values.namespacePrefix }}.Infrastructure.InMemory;

public class InMemoryMeasurementRepository : IMeasurementRepository
{
    private readonly ConcurrentDictionary<Guid, IMeasurement> _storage = new();

    public InMemoryMeasurementRepository()
    {
    }

    public Task<IMeasurement> AddAsync(IMeasurement measurement, CancellationToken cancellationToken)
    {
        if (measurement == null)
        {
            throw new ArgumentNullException(nameof(measurement));
        }

        _storage.AddOrUpdate(measurement.Id, (_) => measurement, (_, item) => item);

        return Task.FromResult(measurement);
    }

    public Task<IMeasurement?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        _storage.TryGetValue(id, out var measurement);
        return Task.FromResult(measurement);
    }
}