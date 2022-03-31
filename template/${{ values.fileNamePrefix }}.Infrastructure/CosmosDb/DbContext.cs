using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ${{ values.fileNamePrefix }}.Infrastructure.CosmosDb;
public class MeasurementContext : DbContext
{
    public MeasurementContext(DbContextOptions<MeasurementContext> options) : base(options)
    {
    }
    public DbSet<Measurement> CatalogItems { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
    }
}
