{%- if values.enableEntityFramework %}
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using ${{ values.namespacePrefix }}.Infrastructure.EntityFramework;
{%- endif %}
{%- if values.enableInMemoryRepository %}
global using ${{ values.namespacePrefix }}.Infrastructure.InMemory;
{%- endif %}

global using Moq;
global using ${{ values.namespacePrefix }}.Domain.Models;
global using ${{ values.namespacePrefix }}.Domain.Repositories;
