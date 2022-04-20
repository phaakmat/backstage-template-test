namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class CosmosDbContext : DbContext, IDbContext
{
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
    {
    }

    public async Task<bool> EnsureCreated()
    {
        return await Database.EnsureCreatedAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Measurement>()
            .HasKey(o => o.Id);
    }
}
