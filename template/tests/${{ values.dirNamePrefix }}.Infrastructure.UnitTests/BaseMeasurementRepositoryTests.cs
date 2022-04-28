namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public abstract class BaseMeasurementRepositoryTests
{
    protected async Task ReturnsAddedItem(IMeasurementRepository repository, Guid id, double temperatureC, string summary)
    {
        // Arrange
        var measurement = new Measurement
        {
            Id = id,
            TemperatureC = temperatureC,
            Summary = summary
        };

        // Act
        await repository.CreateAsync(measurement, default(CancellationToken));
        var verify = await repository.FindByIdAsync(measurement.Id, default(CancellationToken));

        // Assert
        Assert.NotNull(verify);
        Assert.Equal(id, verify!.Id);
        Assert.Equal(temperatureC, verify.TemperatureC);
        Assert.Equal(summary, verify.Summary);
    }
}
