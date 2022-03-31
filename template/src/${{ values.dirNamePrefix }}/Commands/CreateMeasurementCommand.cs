namespace ${{ values.fileNamePrefix }}.Commands;

public class CreateMeasurementCommand
{
    public double TemperatureC { get; set; }
    public string? Summary { get; set; }

}
