namespace Funda.FeatureApiTemplate.ErrorHandling;

internal static class WebApplicationExtensions
{
    public static WebApplication UseProblemDetailsExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(applicationBuilder =>
        {
            applicationBuilder.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var error = exceptionHandlerPathFeature?.Error;

                if (error != null)
                {
                    var problemDetails = ProblemDetailsFactory.CreateProblemDetails(context, error);
                    context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(problemDetails, null, "application/problem+json; charset=utf-8");
                }
            });
        });

        return app;
    }
}
