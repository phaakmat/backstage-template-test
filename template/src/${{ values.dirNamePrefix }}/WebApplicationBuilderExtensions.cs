using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Hosting;
using Funda.Extensions.Messaging.Configuration;
using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
using Funda.Extensions.Metrics.Statsd.DependencyResolution;
using Microsoft.AspNetCore.Mvc;

namespace ${{ values.namespacePrefix }};

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Host.AddFundaDefaults("${{ values.applicationName }}");

        // Add services to the container.

        builder.Services.AddHealthChecks();

        builder.Services.AddControllers();

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddFundaMetrics(
            "${{ values.applicationName }}",
            config => config.AddDogstatsd(
                hostname: builder.Configuration["Statsd:Hostname"],
                port: int.Parse(builder.Configuration["Statsd:Port"]),
                prefix: builder.Configuration["Statsd:Prefix"],
                environmentName: builder.Configuration["Statsd:EnvironmentName"]));

        builder.Services.AddFundaDateTimeProvider();

        {% if values.enableFundaMessaging %}
        builder.Services.AddFundaMessaging();
        {% endif %}
        return builder;
    }
}
