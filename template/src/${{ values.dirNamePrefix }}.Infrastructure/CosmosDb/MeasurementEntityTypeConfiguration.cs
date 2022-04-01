using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

internal class MeasurementEntityTypeConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.HasKey(o => o.Id);
        builder.ToTable("measurements", MeasurementContext.DEFAULT_SCHEMA);
    }
}
