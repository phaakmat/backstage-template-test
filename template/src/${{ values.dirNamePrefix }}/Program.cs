using Funda.Extensions.DateTimeProvider;
using Funda.Extensions.HealthChecks;
using Funda.Extensions.Hosting;
using Funda.Extensions.Messaging.Configuration;
using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
using Funda.Extensions.Metrics.Statsd.DependencyResolution;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddFundaDefaults("${{ values.applicationName }}");

// Add services to the container.

builder.Services.AddControllers();
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

builder.Services.AddFundaMessaging();

var app = builder.Build();

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

app.Run();
