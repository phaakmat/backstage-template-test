﻿using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly IDbContext _context;

    public MeasurementRepository(IDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Measurement> Add(Measurement measurement, CancellationToken cancellationToken)
    {
        var entity = _context.Measurements.Add(measurement).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Measurement?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var measurement = await _context.Measurements
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return measurement;
    }
}