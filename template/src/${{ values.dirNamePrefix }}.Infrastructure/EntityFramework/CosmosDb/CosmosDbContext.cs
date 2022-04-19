using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
using ${{ values.namespacePrefix }}.Domain;

public class CosmosDbContext : DbContext, IDbContext
{
    public DbSet<Measurement> Measurements => Set<Measurement>();
    public async Task<bool> EnsureCreated()
    {
        return await Database.EnsureCreatedAsync();
    }

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Measurement>()
            .HasKey(o => o.Id);
    }
}
