namespace ${{ values.namespacePrefix }}.Domain;

public interface IMeasurement
{
    Guid Id { get; set; }

    DateTimeOffset Created { get; set; }

    double TemperatureC { get; set; }

    string? Summary { get; set; }
}
