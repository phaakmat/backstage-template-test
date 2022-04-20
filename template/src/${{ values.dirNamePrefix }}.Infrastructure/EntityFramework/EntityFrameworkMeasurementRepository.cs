namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class EntityFrameworkMeasurementRepository : IMeasurementRepository
{
    private readonly IDbContext _context;

    public EntityFrameworkMeasurementRepository(IDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IMeasurement> AddAsync(IMeasurement measurement, CancellationToken cancellationToken)
    {
        var entity = _context.Measurements.Add(new Measurement(measurement)).Entity;
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