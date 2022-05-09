namespace ${{ values.namespacePrefix }}.ErrorHandling;

internal static class ProblemDetailsFactory
{
    public static HttpValidationProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
    {
        var problemDetails = exception switch
        {
            ValidationException validationException => From(validationException),
            _ => From(exception)
        };

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        return problemDetails;
    }

    private static HttpValidationProblemDetails From(ValidationException validationException)
    {
        var errors = validationException?.Errors
            .GroupBy(o => o.PropertyName, o => o.ErrorMessage)
            .ToDictionary(o => o.Key, o => o.ToArray()) ?? new();

        return new HttpValidationProblemDetails(errors)
        {
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest
        };
    }

    private static HttpValidationProblemDetails From(Exception exception)
    {
        return new HttpValidationProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = exception.Message
        };
    }
}
