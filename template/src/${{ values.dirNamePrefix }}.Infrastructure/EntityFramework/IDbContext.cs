namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public interface IDbContext
{
    DbSet<Measurement> Measurements { get; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
