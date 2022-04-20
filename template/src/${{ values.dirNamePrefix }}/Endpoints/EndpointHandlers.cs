namespace ${{ values.namespacePrefix }}.Endpoints;

public static class EndpointHandlers
{
    public static async Task<object> GetById(Guid id, IMeasurementRepository repository, HttpContext http, CancellationToken token)
    {
        var result = await repository.FindAsync(id, token);

        if (result == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(result);
    }

    public static async Task<object> Create(Measurement item, IMediator mediator, HttpContext http, CancellationToken token)
    {
        var cmd = new CreateMeasurementCommand();
        var result = await mediator.Send(cmd, token);
        return Results.Ok(result);
    }

    public static async Task<object> Delete(Guid id, IMeasurementRepository repository, CancellationToken token)
    {
        return await Task.FromResult(Results.BadRequest());
    }
}