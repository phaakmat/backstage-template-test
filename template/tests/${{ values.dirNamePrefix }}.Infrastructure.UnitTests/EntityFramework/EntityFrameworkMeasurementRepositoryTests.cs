namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class EntityFrameworkMeasurementRepositoryTests : BaseMeasurementRepositoryTests, IClassFixture<EntityFrameworkMeasurementRepositoryFixture>
{
    private readonly EntityFrameworkMeasurementRepositoryFixture _fixture;

    public EntityFrameworkMeasurementRepositoryTests(EntityFrameworkMeasurementRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [ClassData(typeof(MeasurementsTheoryData))]
    public async Task ReturnsAddItem(Guid id, double temperatureC, string summary)
        => await ReturnsAddedItem(_fixture.Repository, id, temperatureC, summary);
}
