namespace ${{ values.namespacePrefix }}.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    private static string Prefix { get; } = "/api/v1/";
    private static string Route(string routeTemplate = "") => $"{Prefix}{routeTemplate}";

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route("{id:guid}"), EndpointHandlers.FindById)
            .Produces<IMeasurement>(200)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapPost(Route(), EndpointHandlers.Create)
            .Accepts<CreateMeasurementCommand>("application/json")
            .Produces<IMeasurement>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();

        app.MapDelete(Route("{id:guid}"), EndpointHandlers.Delete)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        
        return app;
    }
}
