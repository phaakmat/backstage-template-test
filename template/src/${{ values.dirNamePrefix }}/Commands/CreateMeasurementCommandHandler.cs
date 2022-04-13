using MediatR;

{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging;
{%- endif %}
using ${{ values.namespacePrefix }}.Domain;
namespace ${{ values.namespacePrefix }}.Commands;

public class CreateMeasurementCommandHandler
    : IRequestHandler<CreateMeasurementCommand, bool>
{%- if values.enableFundaMessaging %},
      IMessageHandler
{%- endif %}
{
    private readonly IMeasurementRepository _repository;
    private readonly IMediator _mediator;
    private readonly ILogger<CreateMeasurementCommandHandler> _logger;

    public CreateMeasurementCommandHandler(IMediator mediator,
        IMeasurementRepository repository,
        ILogger<CreateMeasurementCommandHandler> logger)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateMeasurementCommand message, CancellationToken cancellationToken)
    {
        var entity = new Measurement(Guid.NewGuid(), DateTimeOffset.UtcNow, message.TemperatureC, message.Summary);

        await _repository.Add(entity, cancellationToken);

        return true;
    }

    {%- if values.enableFundaMessaging %}
    public Task HandleAsync(IMessage message, IMessageHandlerContext context, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }
    {%- endif %}
}
