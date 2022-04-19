using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Domain.Models;

namespace ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;

public interface IDbContext
{
    DbSet<Measurement> Measurements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<bool> EnsureCreated();
}
