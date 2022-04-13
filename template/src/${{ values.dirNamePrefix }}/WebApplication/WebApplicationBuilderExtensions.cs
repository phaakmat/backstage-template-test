using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Hosting;
using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
using Funda.Extensions.Metrics.Statsd.DependencyResolution;
using Microsoft.AspNetCore.Mvc;
using MediatR;
{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging.Metrics;
using Funda.Extensions.Messaging.DatadogTracing;
using Funda.Extensions.Messaging.Azure;
using Funda.Extensions.Messaging.Configuration;
{%- endif %}
{%- if values.enableCosmosDb %}
using ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
{%- endif %}
{%- if values.enableEntityFramework %}
using Microsoft.EntityFrameworkCore;
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}
using ${{ values.namespacePrefix }}.Domain;
using ${{ values.namespacePrefix }}.Commands;

namespace ${{ values.namespacePrefix }}.Extensions;

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

        builder.Services.AddAuthorization();

        {%- if values.enableMvcControllers %}
        // MVC Controllers

        builder.Services.AddControllers();
        {%- endif %}

        {%- if values.enableFundaMessaging %}
        // Funda.Messaging

        builder.Services.AddFundaDateTimeProvider();

        builder.Services
            .AddFundaMessaging()
            .AddFundaMessagingAzureServiceBus()
            .AddDatadogTracing()
            .ConfigureEndpoint("Commands", endpoint =>
            {
                endpoint
                    .ConfigurePubSub<CreateMeasurementCommand, CreateMeasurementCommandHandler>()
                    .ConfigureAzureServiceBusQueue("${{ values.applicationName }}", options =>
                        builder.Configuration.GetSection("AzureServiceBus").Bind(options))
                    .ConfigureAzureServiceBusWorker()
                    .ConfigurePipeline(pipeline =>
                    {
                        pipeline
                            .UsePublishMetrics()
                            .UseRetryMessageExecution()
                            .UseDatadogTracing()
                            .UseHandleMessage();
                    });
            });
        {%- endif %}
        
        {%- if values.enableCosmosDb %}
        // Cosmos DB
        builder.Services.AddCosmosDbInfrastructure(options =>
            builder.Configuration.GetSection("CosmosDb").Bind(options)
        );
        {%- endif %}
           
        {%- if values.enableEntityFramework %}
        // Entity Framework

        builder.Services.AddScoped<IMeasurementRepository, EfMeasurementRepository>();

        {%- if values.enableSqlServer %}
        // SQL Server

        builder.Services
            .AddDbContext<IDbContext, SqlServerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        {%- endif %}

        {%- endif %}

        builder.Services.AddMediatR(typeof(Program).Assembly);

        return builder;
    }
}
