using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ${{ values.fileNamePrefix }}.Infrastructure.CosmosDb;


public class MeasurementRepository : IBuyerRepository, IMeasurementRepository
{
    private readonly MeasurementContext _context;
    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public MeasurementRepository(MeasurementContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Measurement Add(Measurement measurement)
    {
        return _context.Measurements.Add(measurement).Entity;
    }

    public async Task<Measurement> FindAsync(string id)
    {
        var measurement = await _context.Measurements
            .Where(o => o.Id == buyerIdentityGuid)
            .SingleOrDefaultAsync();

        return measurement;
    }
}