namespace ${{ values.namespacePrefix }}.Domain;

public class Measurement : IMeasurement
{
    public Measurement()
    {
    }
    public Measurement(Guid id, DateTimeOffset created, double temperatureC, string? summary)
    {
        Id = id;
        Created = created;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public Guid Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public double TemperatureC { get; set; }

    public double TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
