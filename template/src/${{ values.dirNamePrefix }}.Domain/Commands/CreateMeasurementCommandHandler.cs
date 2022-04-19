using Microsoft.Extensions.Logging;
using MediatR;
using ${{ values.namespacePrefix }}.Domain.Repositories;
using ${{ values.namespacePrefix }}.Domain.Models;

namespace ${{ values.namespacePrefix }}.Domain.Commands;

public class CreateMeasurementCommandHandler : IRequestHandler<CreateMeasurementCommand, IMeasurement>
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
}
