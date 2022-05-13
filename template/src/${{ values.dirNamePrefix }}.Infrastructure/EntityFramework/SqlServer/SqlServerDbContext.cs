namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class SqlServerDbContext : DbContext, IDbContext
{
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Measurement>()
            .HasKey(o => o.Id);
    }
}
