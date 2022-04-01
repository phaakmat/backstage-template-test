using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework.CosmosDb;
public class MeasurementContext : DbContext, IDbContext
{
    public const string DEFAULT_SCHEMA = "schema";
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public MeasurementContext(DbContextOptions<MeasurementContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "OrdersDB");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MeasurementEntityTypeConfiguration());
    }
}
