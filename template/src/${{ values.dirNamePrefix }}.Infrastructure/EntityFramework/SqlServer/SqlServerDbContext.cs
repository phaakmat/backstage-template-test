using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain.Models;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public class SqlServerDbContext : DbContext, IDbContext
{
    public DbSet<Measurement> Measurements => Set<Measurement>();

    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
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
