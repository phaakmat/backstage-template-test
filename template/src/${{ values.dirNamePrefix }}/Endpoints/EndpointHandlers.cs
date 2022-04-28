namespace ${{ values.namespacePrefix }}.Endpoints;

public static class EndpointHandlers
{
    public static async Task<IResult> FindById(Guid id, IMeasurementRepository repository, HttpContext http, CancellationToken token)
    {
        var result = await repository.FindByIdAsync(id, token);

        return result == null ? Results.NoContent() : Results.Ok(result);
    }

    public static async Task<IResult> Create(CreateMeasurementCommand command, IMediator mediator, HttpContext http, CancellationToken token)
    {
        var result = await mediator.Send(command, token);

        return Results.Ok(result);
    }

    public static async Task<IResult> Delete(Guid id, IMediator mediator, CancellationToken token)
    {
        var cmd = new DeleteMeasurementCommand
        {
            MeasurementId = id
        };
        var found = await mediator.Send(cmd, token);

        return found ? Results.Ok() : Results.NoContent();
    }
}