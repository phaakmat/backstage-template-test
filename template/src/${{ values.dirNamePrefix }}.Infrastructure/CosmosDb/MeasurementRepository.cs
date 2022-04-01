using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly MeasurementContext _context;

    public MeasurementRepository(MeasurementContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Measurement Add(Measurement measurement)
    {
        return _context.Measurements.Add(measurement).Entity;
    }

    public async Task<Measurement> FindAsync(Guid id)
    {
        var measurement = await _context.Measurements
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync();

        return measurement;
    }
}