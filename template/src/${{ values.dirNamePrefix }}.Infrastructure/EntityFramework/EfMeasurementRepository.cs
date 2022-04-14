using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class EfMeasurementRepository : IMeasurementRepository
{
    private readonly IDbContext _context;

    public EfMeasurementRepository(IDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IMeasurement> AddAsync(Measurement measurement, CancellationToken cancellationToken)
    {
        var entity = _context.Measurements.Add(measurement).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IMeasurement?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _context.Measurements
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return item;
    }
}