using Funda.Extensions.HealthChecks;

using ${{ values.namespacePrefix }};

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseFundaHealthChecks();

        return app;
    }
}
