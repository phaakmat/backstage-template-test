namespace ${{ values.namespacePrefix }}.Domain;

public class Measurement : IMeasurement
{
    public Measurement(Guid id, DateTimeOffset created, int temperatureC, string? summary)
    {
        Id = id;
        Created = created;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public Guid Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
