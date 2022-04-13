using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
using ${{ values.namespacePrefix }}.Domain;

public class CosmosDbContext : DbContext, IDbContext
{
    public const string DEFAULT_SCHEMA = "schema";
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Measurement>()
            .HasKey(o => o.Id);
    }
}
