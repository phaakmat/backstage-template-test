namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class CosmosDbContext : DbContext, IDbContext
{
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
