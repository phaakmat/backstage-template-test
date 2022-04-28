namespace ${{ values.namespacePrefix }}.Startup;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        {%- if values.enableControllers %}

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        {%- endif %}
        {%- if values.enableEndpoints %}

        app.MapEndpoints();
        {%- endif %}

        app.UseFundaHealthChecks();

        {%- if values.enableEntityFramework %}

        if (app.Environment.IsDevelopment())
        {
            app.Services.EnsureEntityFrameworkDbCreated();
        }
        {%- endif %}
        app.UseProblemDetailsExceptionHandler();

        return app;
    }
}
