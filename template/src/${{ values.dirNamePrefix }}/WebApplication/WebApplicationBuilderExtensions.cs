using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.Hosting;
using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
using Funda.Extensions.Metrics.Statsd.DependencyResolution;
{%- if values.enableFundaMessaging %}
using Funda.Extensions.Messaging.Metrics;
using Funda.Extensions.Messaging.DatadogTracing;
using Funda.Extensions.Messaging.Azure;
using Funda.Extensions.Messaging.Configuration;
using ${{ values.namespacePrefix }}.Messaging;
{%- endif %}
{%- if values.enableCosmosDb %}
using ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
{%- endif %}
{%- if values.enableEntityFramework %}
using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}
{%- if values.enableInMemoryRepository %}
using ${{ values.namespacePrefix }}.Infrastructure.InMemory;
{%- endif %}
using ${{ values.namespacePrefix }}.Domain.Commands;
using ${{ values.namespacePrefix }}.Domain.Models;
using ${{ values.namespacePrefix }}.Domain.Repositories;

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
        builder.AddFundaMessaging();
		{%- endif %}
		
		{%- if values.enableCosmosDb %}
        
		// Add Cosmos DB
        builder.Services.AddCosmosDbInfrastructure(options =>
            builder.Configuration.GetSection("CosmosDb").Bind(options)
        );
        {%- endif %}

        {%- if values.enableEntityFramework %}
		
        // Add repository based on Entity Framework
        builder.Services.AddScoped<IMeasurementRepository, EntityFrameworkMeasurementRepository>();
		{%- endif %}

        {%- if values.enableInMemoryRepository %}

        // Add repository
        builder.Services.AddSingleton<IMeasurementRepository, InMemoryMeasurementRepository>();
        {%- endif %}


        {%- if values.enableEntityFrameworkSqlServer %}

		// Configure EntityFramework to use SQL Server
        builder.Services
            .AddDbContext<IDbContext, SqlServerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        {%- elif values.enableEntityFrameworkCosmos %}

		// Configure EntityFramework to use CosmosDb
        builder.Services.AddDbContext<IDbContext, CosmosDbContext>(options =>
            options.UseCosmos(builder.Configuration["CosmosDb:Endpoint"],
                builder.Configuration["CosmosDb:PrimaryKey"],
                builder.Configuration["CosmosDb:DatabaseId"]));
		{%- endif %}

        builder.Services.AddMediatR(typeof(Program).Assembly);

        return builder;
    }
}
