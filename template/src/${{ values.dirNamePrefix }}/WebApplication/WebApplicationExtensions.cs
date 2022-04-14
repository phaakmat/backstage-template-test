using Funda.Extensions.HealthChecks;

{%- if values.enableEndpoints %}
using ${{ values.namespacePrefix }}.Endpoints;
{%- endif %}

namespace ${{ values.namespacePrefix }}.Extensions;

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


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        {% if values.enableMvcControllers %}
        app.MapControllers();
        {% endif %}
        {%- if values.enableEndpoints %}
        app.MapEndpoints();
        {%- endif %}

        app.UseFundaHealthChecks();

        return app;
    }
}
