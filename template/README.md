# ${{ values.repoName }}

${{ values.description }}

# How to use

After installing the [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) you can use the standard `dotnet` CLI commands to build/test/run this service, for example:

```shell
$ dotnet build
$ dotnet test
$ dotnet run -p src/${{ dirNamePrefix }}/${{ fileNamePrefix }}.csproj
```

# Overview

## At a glance

- [Feature Service API](https://backstage.funda.io/docs/default/Component/golden-paths-feature-api) Golden Path
- [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
- [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
- [Global usings](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#global-modifier)
- [File scoped namespaces](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace)

## Projects 

Key projects in the [src](./src) directory:

| Project | Description |
| --- | --- |
| [${{ values.dirNamePrefix }}](./src/${{ values.dirNamePrefix }}) | Application entry point. See [Program.cs](./src/${{ values.dirNamePrefix }}/Program.cs) and the [Startup](./src/${{ values.dirNamePrefix }}/Startup) folder. |
| [${{ values.dirNamePrefix }}.Domain](./src/${{ values.dirNamePrefix }}.Domain) | Domain models, commands, etc. |
| [${{ values.dirNamePrefix }}.Infrastructure](./src/${{ values.dirNamePrefix }}.Infrastructure) | Storage and other infrastructure support. |

## Backstage

The file [catalog-info.yaml](./catalog-info.yaml)  provides [Backstage](https://backstage.io/) catalog integration ([docs](https://backstage.io/docs/features/software-catalog/descriptor-format#kind-component)).

{%- if values.enableEndpoints %}
## .NET 6 Endpoints

Endpoints are defined in the `Endpoints` directory. 

{%- endif %}

{%- if values.enableControllers %}
## MVC Controllers

ASP.NET MVC Controllers are defined in the `Controllers` directory.

{%- endif %}

{%- if values.enableFundaMessaging %}
## Funda Messaging

The `Messaging` directory contains configuration for the [Funda.Extensions.Messaging](https://git.funda.nl/projects/PACKAGES/repos/funda.extensions.messaging) package.

{%- endif %}

{%- if values.enableSqlServer %}
## SQL Server


{%- endif %}

{%- if values.enableCosmosDb %}

## CosmosDB
{%- endif %}

{%- if values.enableEntityFramework %}
## Entity Framework Core

[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) is an object-relational mapper (ORM) for database access. See [WebApplicationBuilderExtensions.cs]([src/${{ values.dirNamePrefix }}/](./src/${{ values.dirNamePrefix }}/Startup/WebApplicationBuilderExtensions.cs) and [src/${{ values.dirNamePrefix }}.Infrastructure/EntityFramework](./src/${{ values.dirNamePrefix }}.Infrastructure/EntityFramework).

{%- endif %}

# Owner

This codebase is owned by ${{ values.owner }}

*Generated from template ${{ values.templateName }}*

