namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class InMemoryMeasurementRepositoryFixture : IDisposable
{
    public IMeasurementRepository Repository { get; }

    public InMemoryMeasurementRepositoryFixture()
    {
        Repository = new InMemoryMeasurementRepository();
    }

    public void Dispose()
    {
    }
}
