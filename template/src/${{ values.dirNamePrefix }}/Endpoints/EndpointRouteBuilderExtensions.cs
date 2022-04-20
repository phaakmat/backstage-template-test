namespace ${{ values.namespacePrefix }}.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiVersion + "/{id:guid}", EndpointHandlers.GetById)
            .Produces<IMeasurement>(200)
            .ProducesProblem(404);

        app.MapPost("/", EndpointHandlers.Create)
            .Accepts<Measurement>("application/json")
            .Produces<IMeasurement>(201);

        app.MapDelete("/{id:guid}", EndpointHandlers.Delete);

        return app;
    }

    public static string ApiVersion { get; set; } = "1.0";
}
