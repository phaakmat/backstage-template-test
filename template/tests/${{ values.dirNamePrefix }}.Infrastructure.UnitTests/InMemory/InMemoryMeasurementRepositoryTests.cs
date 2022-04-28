namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class InMemoryMeasurementRepositoryTests : BaseMeasurementRepositoryTests, IClassFixture<InMemoryMeasurementRepositoryFixture>
{
    private readonly InMemoryMeasurementRepositoryFixture _fixture;

    public InMemoryMeasurementRepositoryTests(InMemoryMeasurementRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [ClassData(typeof(MeasurementsTheoryData))]
    public async Task ReturnsAddItem(Guid id, double temperatureC, string summary)
        => await ReturnsAddedItem(_fixture.Repository, id, temperatureC, summary);
}
