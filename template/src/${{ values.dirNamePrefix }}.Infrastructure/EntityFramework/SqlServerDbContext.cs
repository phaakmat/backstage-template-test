using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
public class SqlServerDbContext : DbContext, IDbContext
{
    public const string DEFAULT_SCHEMA = "schema";
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public async Task<bool> EnsureCreated()
    {
        return await Database.EnsureCreatedAsync();
    }

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Measurement>()
            .HasKey(o => o.Id);
    }
}
