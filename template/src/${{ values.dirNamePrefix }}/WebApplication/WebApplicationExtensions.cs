﻿using Funda.Extensions.HealthChecks;

{%- if values.enableEndpoints %}
using ${{ values.namespacePrefix }}.Endpoints;
{%- endif %}
{%- if values.enableMessaging %}
using ${{ values.namespacePrefix }}.Messaging;
{%- endif %}
{%- if values.enableEntityFramework %}
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}
namespace ${{ values.namespacePrefix }}.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
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

        {%- if values.enableEntityFramework %}
        if (app.Environment.IsDevelopment())
        {
            app.Services.EnsureEntityFrameworkDbCreated();
        }
        {%- endif %}

        return app;
    }
}
