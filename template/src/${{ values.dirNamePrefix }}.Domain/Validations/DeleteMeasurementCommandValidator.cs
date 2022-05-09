namespace ${{ values.namespacePrefix }}.Domain.Validations;

public class DeleteMeasurementCommandValidator : AbstractValidator<DeleteMeasurementCommand>
{
    public DeleteMeasurementCommandValidator(ILogger<DeleteMeasurementCommandValidator> logger)
    {
        RuleFor(command => command.MeasurementId).NotEmpty();
    }
}