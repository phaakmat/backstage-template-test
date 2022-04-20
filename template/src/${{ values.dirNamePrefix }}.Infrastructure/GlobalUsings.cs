global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.DependencyInjection;
global using ${{ values.namespacePrefix }}.Domain.Models;
global using ${{ values.namespacePrefix }}.Domain.Repositories;
global using ${{ values.namespacePrefix }}.Domain.Commands;

{%- if values.enableEntityFramework %}
global using Microsoft.EntityFrameworkCore;
global using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}

{%- if values.enableCosmosDb %}
global using Microsoft.Azure.Cosmos;
global using ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;
{%- endif %}

{%- if values.enableInMemoryRepository %}
global using ${{ values.namespacePrefix }}.Infrastructure.InMemory;
{%- endif %}
