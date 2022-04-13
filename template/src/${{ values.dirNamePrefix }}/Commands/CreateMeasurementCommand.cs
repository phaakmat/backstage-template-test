using MediatR;
{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging.CQRS;
{%- endif %}
namespace ${{ values.namespacePrefix }}.Commands;

public class CreateMeasurementCommand :
{%- if values.enableFundaMessaging %}
    Command,
{%- endif %}
    IRequest<bool>
{
    public double TemperatureC { get; set; }
    public string? Summary { get; set; }

}
