using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ${{ values.namespacePrefix }}.Domain.Models;
using ${{ values.namespacePrefix }}.Domain.Commands;
using ${{ values.namespacePrefix }}.Domain.Repositories;

namespace ${{ values.namespacePrefix }}.Endpoints;

public static class MeasurementEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("/{id:guid}",
                async (Guid id, IMeasurementRepository repository, HttpContext http, CancellationToken token) =>
                {
                    var result = await repository.FindAsync(id, token);

                    if (result == null)
                    {
                        return Results.NotFound();
                    }

                    return Results.Ok(result);
                })
            .Produces(200, typeof(IMeasurement))
            .ProducesProblem(404)
            .AllowAnonymous();

        app.MapPost("/", 
                async ([FromBody] Measurement item, IMediator mediator, HttpContext http, CancellationToken token) =>
                {
                    var cmd = new CreateMeasurementCommand();
                        var result = await mediator.Send(cmd, token);

                    if (result == null)
                    {
                        return Results.NotFound();
                    }

                    return Results.Ok(result);
                })
            .Accepts<Measurement>("application/json")
            .Produces(201, typeof(IMeasurement))
            .ProducesProblem(404);

        app.MapDelete("/{id:guid}",
                async (Guid id, IMeasurementRepository repository, CancellationToken token) =>
                {
                    return await Task.FromResult(Results.BadRequest());
                })
            .RequireAuthorization();

        return app;
    }
}
