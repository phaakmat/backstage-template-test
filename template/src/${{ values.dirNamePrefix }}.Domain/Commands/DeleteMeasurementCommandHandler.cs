namespace ${{ values.namespacePrefix }}.Domain.Commands;

public class DeleteMeasurementCommandHandler : IRequestHandler<DeleteMeasurementCommand, bool>
{
    private readonly IMeasurementRepository _repository;
    private readonly ILogger<DeleteMeasurementCommandHandler> _logger;
    private readonly IValidator<DeleteMeasurementCommand> _validator;

    public DeleteMeasurementCommandHandler(
        IMeasurementRepository repository,
        ILogger<DeleteMeasurementCommandHandler> logger,
        IValidator<DeleteMeasurementCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<bool> Handle(DeleteMeasurementCommand command, CancellationToken cancellationToken)
    {
        var validationResults = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResults.IsValid)
        {
            throw new ValidationException(validationResults.Errors);
        }

        return await _repository.DeleteAsync(command.MeasurementId, cancellationToken);
    }
}
