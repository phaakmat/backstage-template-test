﻿namespace ${{ values.namespacePrefix }}.Domain.Repositories;

public interface IMeasurementRepository
{
    Task<IMeasurement> AddAsync(IMeasurement measurement, CancellationToken cancellationToken);
    Task<IMeasurement?> FindAsync(Guid id, CancellationToken cancellationToken);
}
