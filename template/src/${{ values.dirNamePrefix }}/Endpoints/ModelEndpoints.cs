namespace ${{ values.fileNamePrefix }}.Endpoints;

public static class Endpoints
{
    public static WebApplicationBuilder AddEndpoints(this WebApplicationBuilder builder)
    {
        return builder;
    }

    public static WebApplication UseEndpoints(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
