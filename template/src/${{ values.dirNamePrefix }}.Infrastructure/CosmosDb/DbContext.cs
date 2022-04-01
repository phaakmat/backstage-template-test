using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
public class MeasurementContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";
    public MeasurementContext(DbContextOptions<MeasurementContext> options) : base(options)
    {
    }
    public DbSet<Measurement> Measurements { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MeasurementEntityTypeConfiguration());
    }
}
