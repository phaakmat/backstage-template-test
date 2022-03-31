using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ${{ values.fileNamePrefix }}.Infrastructure.CosmosDb;

internal class MeasurementEntityTypeConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.HasKey(o => o.Id);
        builder.ToTable("measurement", MeasurementContext.DEFAULT_SCHEMA);
    }
}
