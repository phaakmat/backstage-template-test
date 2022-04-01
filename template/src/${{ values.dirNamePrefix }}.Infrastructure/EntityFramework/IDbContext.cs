using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public interface IDbContext
{
    DbSet<Measurement> Measurements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
