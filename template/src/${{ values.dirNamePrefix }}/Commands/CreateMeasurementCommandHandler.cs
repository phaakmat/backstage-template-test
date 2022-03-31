using MediatR;

namespace ${{ values.fileNamePrefix }}.Commands;

public class CreateMeasurementCommandHandler
    : IRequestHandler<CreateMeasurementCommand, bool>
{
    private readonly IMeasurementRepository _repository;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
    private readonly ILogger<CreateMeasurementCommandHandler> _logger;

    // Using DI to inject infrastructure persistence Repositories
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
        // Add Integration event to clean the basket
        var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(message.UserId);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStartedIntegrationEvent);

        var mesaurement = new Measurement(1, DateTimeOffset.UtcNow, message.TemperatureC, message.Summary);

        // Add/Update the Buyer AggregateRoot
        // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
        // methods and constructor so validations, invariants and business logic 
        // make sure that consistency is preserved across the whole aggregate
        var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
        var order = new Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber, message.CardSecurityNumber, message.CardHolderName, message.CardExpiration);

        foreach (var item in message.OrderItems)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
        }

        _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

        _repository.Add(order);

        return await _orderRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
