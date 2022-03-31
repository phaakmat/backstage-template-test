using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Hosting;
using Funda.Extensions.Messaging.Configuration;
using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
using Funda.Extensions.Metrics.Statsd.DependencyResolution;
using Microsoft.AspNetCore.Mvc;

{%- if values.enableCosmosDb %}
using ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
{%- endif %}

namespace ${{ values.namespacePrefix }};

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        builder.Host.AddFundaDefaults("${{ values.applicationName }}");

        // Add services to the container.

        builder.Services.AddHealthChecks();
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

        {%- if values.enableMvcControllers %}
        builder.AddControllers();
        {%- endif %}

        {%- if values.enableFundaMessaging %}
        builder.AddFundaMessaging();
        {%- endif %}

        {%- if values.enableEndpoints %}
        builder.AddEndpoints();
        {%- endif %}

        {%- if values.enableCosmosDb %}
        builder.AddCosmosDb();
        {%- endif %}

        return builder;
    }

    {%- if values.enableFundaMessaging %}
    private static void AddFundaMessaging(this WebApplicationBuilder builder)
    {
        builder.Services.AddFundaDateTimeProvider();

        builder.Services.AddFundaMessaging();
    }
    {%- endif %}

    {%- if values.enableMvcControllers %}
    private static void AddControllers(this WebApplicationBuilder builder)
    {
        builder.AddControllers();
    }
    {%- endif %}

    {%- if values.enableEndpoints %}
    private static void AddEndpoints(this WebApplicationBuilder builder)
    {

    }
    {%- endif %}

    {%- if values.enableCosmosDb %}
    private static void AddCosmosDb(this WebApplicationBuilder builder)
    {
        builder.Services.AddCosmosDbInfrastructure(options =>
            builder.Configuration.GetSection(nameof(Options)).Bind(options)
        );
    }
    {%- endif %}
}
