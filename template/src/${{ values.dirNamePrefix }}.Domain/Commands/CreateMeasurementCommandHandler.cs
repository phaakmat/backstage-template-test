namespace ${{ values.namespacePrefix }}.Domain.Commands;

public class CreateMeasurementCommandHandler : IRequestHandler<CreateMeasurementCommand, IMeasurement>
{
    private readonly IMeasurementRepository _repository;
    private readonly ILogger<CreateMeasurementCommandHandler> _logger;
    private readonly IValidator<CreateMeasurementCommand> _validator;

    public CreateMeasurementCommandHandler(
        IMeasurementRepository repository,
        ILogger<CreateMeasurementCommandHandler> logger,
        IValidator<CreateMeasurementCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<IMeasurement> Handle(CreateMeasurementCommand command, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResults.IsValid)
        {
            _logger.LogError("Validation errors: {errors}", validationResults.Errors);
            throw new ValidationException(validationResults.Errors);
        }

        var entity = new Measurement(
            command.MeasurementId ?? Guid.NewGuid(), 
            DateTimeOffset.UtcNow, 
            command.TemperatureC, 
            command.Summary
        );

        return await _repository.CreateAsync(entity, cancellationToken);
    }
}
