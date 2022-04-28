global using System.Diagnostics;

global using MediatR;
global using FluentValidation;
global using FluentValidation.AspNetCore;

global using Funda.Extensions.Hosting;
global using Funda.Extensions.HealthChecks;
global using Funda.Extensions.Metrics.Abstractions.DependencyResolution;
global using Funda.Extensions.Metrics.Statsd.DependencyResolution;

global using ${{ values.namespacePrefix }}.ErrorHandling;
global using ${{ values.namespacePrefix }}.Domain.Models;
global using ${{ values.namespacePrefix }}.Domain.Repositories;
global using ${{ values.namespacePrefix }}.Domain.Commands;
global using ${{ values.namespacePrefix }}.Domain.Validations;

{%- if values.enableEntityFramework %}
global using Microsoft.EntityFrameworkCore;
global using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}

{%- if values.enableEntityFrameworkSqlServer %}
global using Microsoft.EntityFrameworkCore.SqlServer;
{%- endif %}

{%- if values.enableCosmosDb %}
global using ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
{%- endif %}

{%- if values.enableInMemoryRepository %}
global using ${{ values.namespacePrefix }}.Infrastructure.InMemory;
{%- endif %}

{%- if values.enableControllers %}
global using Microsoft.AspNetCore.Mvc;
global using ${{ values.namespacePrefix }}.Controllers;
{%- endif %}

{%- if values.enableEndpoints %}
global using ${{ values.namespacePrefix }}.Endpoints;
{%- endif %}

{%- if values.enableFundaMessaging %}
global using Funda.Extensions.DateTimeProvider;
global using Funda.Extensions.Messaging;
global using Funda.Extensions.Messaging.CQRS;
global using Funda.Extensions.Messaging.InMemory;
global using Funda.Extensions.Messaging.Configuration;
global using Funda.Extensions.Messaging.DatadogTracing;
global using Funda.Extensions.Messaging.Metrics;
global using ${{ values.namespacePrefix }}.Messaging;
{%- endif %}
global using Microsoft.AspNetCore.Diagnostics;

// ProblemDetails is in the Mvc namespace but actually lives in Microsoft.AspNetCore.Http.Extensions.dll
global using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;
