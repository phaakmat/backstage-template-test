using MediatR;

{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging;
{%- endif %}
using ${{ values.namespacePrefix }}.Domain;
namespace ${{ values.namespacePrefix }}.Commands;

public class CreateMeasurementCommandHandler
    : IRequestHandler<CreateMeasurementCommand, IMeasurement>
{%- if values.enableFundaMessaging %},
      IMessageHandler
{%- endif %}
{
    private readonly IMeasurementRepository _repository;
    private readonly ILogger<CreateMeasurementCommandHandler> _logger;

    public CreateMeasurementCommandHandler(
        IMeasurementRepository repository,
        ILogger<CreateMeasurementCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IMeasurement> Handle(CreateMeasurementCommand message, CancellationToken cancellationToken)
    {
        var entity = new Measurement(Guid.NewGuid(), DateTimeOffset.UtcNow, message.TemperatureC, message.Summary);

        return await _repository.AddAsync(entity, cancellationToken);
    }

    {%- if values.enableFundaMessaging %}
    public Task HandleAsync(IMessage message, IMessageHandlerContext context, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }
    {%- endif %}
}
