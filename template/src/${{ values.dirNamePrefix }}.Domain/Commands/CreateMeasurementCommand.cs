using MediatR;
{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging.CQRS;
{%- endif %}
using ${{ values.namespacePrefix }}.Domain;

namespace ${{ values.namespacePrefix }}.Commands;

public class CreateMeasurementCommand :
{%- if values.enableFundaMessaging %}
    Command,
{%- endif %}
    IRequest<IMeasurement>
{
    public double TemperatureC { get; set; }
    public string? Summary { get; set; }

}
