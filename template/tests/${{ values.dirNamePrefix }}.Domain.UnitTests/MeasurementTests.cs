namespace ${{ values.namespacePrefix }}.Domain.UnitTests;

public class MeasurementTests
{
    [Fact]
    public void TemperatureC_Matches_TemperatureF()
    {
        // Arrange
        var sut = new Measurement
        {
            TemperatureC = 0
        };

        // Act
        var temperatureF = sut.TemperatureF;

        // Assert
        Assert.Equal(32, temperatureF);
    }
}
