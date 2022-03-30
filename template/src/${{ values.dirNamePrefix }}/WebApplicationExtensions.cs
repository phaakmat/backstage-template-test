using Funda.Extensions.HealthChecks;

namespace ${{ values.namespacePrefix }};

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
        {% if values.enableMvcControllers %}
        app.MapControllers();
        {% endif %}

        app.UseFundaHealthChecks();

        return app;
    }
}
