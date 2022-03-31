namespace ${{ values.namespacePrefix }}.Domain;

public interface IMeasurement
{
    public Guid Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
