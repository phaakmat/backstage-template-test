namespace ${{ values.namespacePrefix }}.Domain;

public interface IModel
{
    DateTime Date { get; set; }

    int TemperatureC { get; set; }

    int TemperatureF { get; }

    string? Summary { get; set; }
}
