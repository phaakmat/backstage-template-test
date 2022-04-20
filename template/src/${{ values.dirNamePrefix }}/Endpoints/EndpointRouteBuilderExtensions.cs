namespace ${{ values.namespacePrefix }}.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        static async Task<object> SomeMethod(Guid id, IMeasurementRepository repository, HttpContext http, CancellationToken token) 
        {
            var result = await repository.FindAsync(id, token);

            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        }

        app.MapGet(ApiVersion + "/{id:guid}", SomeMethod)
            .Produces<IMeasurement>(200)
            .ProducesProblem(404);

        app.MapPost("/",
                async (Measurement item, IMediator mediator, HttpContext http, CancellationToken token) =>
                {
                    var cmd = new CreateMeasurementCommand();
                    var result = await mediator.Send(cmd, token);
                    return Results.Ok(result);
                })
            .Accepts<Measurement>("application/json")
            .Produces<IMeasurement>(201);

        app.MapDelete("/{id:guid}",
            async (Guid id, IMeasurementRepository repository, CancellationToken token) =>
            {
                return await Task.FromResult(Results.BadRequest());
            });

        return app;
    }

    public static string ApiVersion { get; set; } = "1.0";
}
