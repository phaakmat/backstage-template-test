# ${{ values.repoName }}

${{ values.description }}

# Getting started

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
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)

## Filesystem layout

- [src/${{ values.dirNamePrefix }}/](./src/${{ values.dirNamePrefix }})
: Application entry point. See [Program.cs](./src/${{ values.dirNamePrefix }}/Program.cs) and the [Startup](./src/${{ values.dirNamePrefix }}/Startup) folder.
- [src/${{ values.dirNamePrefix }}.Domain/](./src/${{ values.dirNamePrefix }}.Domain)
: Domain classes, models, commands, validation logic, etc.
- [src/${{ values.dirNamePrefix }}.Infrastructure/](./src/${{ values.dirNamePrefix }}.Infrastructure)
: Storage and other infrastructure support.
- [tests/](./tests)
: Unit and component tests.
- [${{ values.fileNamePrefix }}.sln](./${{ values.fileNamePrefix }}.sln)
: Solution file.
- [azure-pipelines.yml](./azure-pipelines.yml)
: Azure build pipeline.
- [catalog-info.yaml](./catalog-info.yaml)
: Backstage catalog information.
- [Directory.Build.props](./Directory.Build.props)
: Common MSBuild properties.
- [Dockerfile](./src/Dockerfile)
: Commands to assemble Docker image.
- [NuGet.Config](./NuGet.config)
: NuGet sources.

## appsettings

The different `appsettings.{_}.json` files refer to the following environments, selected by the environment variable `ASPNETCORE_ENVIRONMENT`:

- **Development**
: The local development environment (see also [launchSettings.json](src/${{ values.dirNamePrefix }}/Properties/launchSettings.json)).
- **dev**
: funda dev environment
- **acc**
: funda acceptance
- **prod**
: funda production

## Backstage

The file [catalog-info.yaml](./catalog-info.yaml)  provides [Backstage](https://backstage.io/) catalog integration ([docs](https://backstage.io/docs/features/software-catalog/descriptor-format#kind-component)).

{%- if values.enableEndpoints %}
## .NET 6 Endpoints

Endpoints provide lightweight HTTP handlers. They are defined in the [Endpoints](./src/${{ values.dirNamePrefix }}/Endpoints) directory. 

{%- endif %}

{%- if values.enableControllers %}
## MVC Controllers

ASP.NET MVC Controllers are defined in the [Controllers](./src/${{ values.dirNamePrefix }}/Controllers) directory.

{%- endif %}

{%- if values.enableFundaMessaging %}
## Funda Messaging

The [Messaging](./src/${{ values.dirNamePrefix }}/Messaging) directory contains configuration for the [Funda.Extensions.Messaging](https://git.funda.nl/projects/PACKAGES/repos/funda.extensions.messaging) package.

{%- endif %}

{%- if values.enableSqlServer %}
## SQL Server

See the `"SqlServer"` section in [appsettings.json](src/${{ values.dirNamePrefix }}/appsettings.json) for the SqlServer connection string.

{%- endif %}

{%- if values.enableCosmosDb %}

## CosmosDB

See the `"CosmosDb"` section in [appsettings.json](src/${{ values.dirNamePrefix }}/appsettings.json).

{%- if values.enableCosmosDbRepository %}
A sample repository for Cosmos DB can be found here: [CosmosDbMeasurementRepository.cs](./src/${{%20values.dirNamePrefix%20}}.Infrastructure/CosmosDb/CosmosDbRepository.cs).
{%- endif %}

For local development, install the [Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator).

{%- endif %}

{%- if values.enableEntityFramework %}
## Entity Framework Core

Entity Framework Core code lives in [Infrastructure/EntityFramework](src/${{ values.dirNamePrefix }}.Infrastructure/EntityFramework/). Initialization happens in [WebApplicationBuilderExtensions.cs](./src/${{ values.dirNamePrefix }}/Startup/WebApplicationBuilderExtensions.cs) and [src/${{ values.dirNamePrefix }}.Infrastructure/EntityFramework](./src/${{ values.dirNamePrefix }}.Infrastructure/EntityFramework).

See the `"EntityFramework"` section in [appsettings.json](src/${{ values.dirNamePrefix }}/appsettings.json) for options.

{%- if values.enableEntityFrameworkRepository %}
A sample repository for Entity Framework Core can be found here: [EntityFrameworkMeasurementRepository.cs](./src/${{%20values.dirNamePrefix%20}}.Infrastructure/EntityFramework/EntityFrameworkMeasurementRepository.cs).
{%- endif %}

{%- if values.enableEntityFrameworkCosmosDb %}
### Cosmos DB

See `"EntityFramework:CosmosDb"` in [appsettings.json](src/${{ values.dirNamePrefix }}/appsettings.json) for options.
{%- endif %}

{%- if values.enableEntityFrameworkSqlServer %}
### Sql Server

See `"EntityFramework:SqlServer"` in [appsettings.json](src/${{ values.dirNamePrefix }}/appsettings.json) for options.

{%- endif %}
{%- endif %}

# Owner

This codebase is owned by ${{ values.owner }}

*Based on template ${{ values.templateName }}*

