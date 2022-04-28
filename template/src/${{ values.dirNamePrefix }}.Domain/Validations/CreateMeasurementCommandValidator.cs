namespace Funda.FeatureApiTemplate.Domain.Validations;

public class CreateMeasurementCommandValidator : AbstractValidator<CreateMeasurementCommand>
{
    public CreateMeasurementCommandValidator(ILogger<CreateMeasurementCommandValidator> logger)
    {
        RuleFor(command => command.TemperatureC).GreaterThanOrEqualTo(-273.15);
        RuleFor(command => command.Summary).MaximumLength(2000);
    }
}