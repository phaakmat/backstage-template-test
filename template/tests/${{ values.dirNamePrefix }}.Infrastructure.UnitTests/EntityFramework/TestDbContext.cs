namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class TestDbContext : DbContext, IDbContext
{
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
}
