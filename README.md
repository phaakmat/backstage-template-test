# dotnet-feature-api template

This is a Backstage template that provides a starting point for building [Feature APIs](https://backstage.funda.io/docs/default/Component/golden-paths-feature-api) based on dotnet 6.0.

## At a glance

This template provides:

- Solution and project structure
- Sample HTTP endpoints `Create`, `FindById`, `Delete`
- Sample entity repository
- Sample unit and component tests
- Azure DevOps build pipeline
- [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)
- [Nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references)
- [Global usings](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#global-modifier)
- [File scoped namespaces](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)

The following optional features can be enabled when deploying the template:

- .NET 6 Endpoints
- MVC Controllers
- SqlServer package references and usings
- CosmosDb package references and usings
- Entity Framework Core package references and usings
- `Funda.Extensions.Messaging` package references and usings

# Getting started

To use this template, go to https://backstage.funda.io/create and choose the *.NET 6 Feature API template*.

If there is no such option, then the template may first need to be registered in the Funda Backstage software catalog:

1. Open the import wizard at https://backstage.funda.io/catalog-import
2. When it prompts for a *Repository URL*, enter the URL to the [template.yaml](./template.yaml) file in the repository.
3. Follow the wizard.

## Wizard

# Filesystem layout

- [template.yaml](#templateyaml)
: Backstage template entity.
- [template](#template-folder) folder
: Scaffolding code.


## `template.yaml`

[template.yaml](./template.yaml) contains a [Backstage template entity](https://backstage.io/docs/features/software-templates/software-templates-index). See [the Backstage documentation](https://backstage.io/docs/features/software-catalog/descriptor-format#kind-template) for more information on template entities.
The main elements are:

- `specs.parameters` Input parameters and prompts.
- `specs.steps` Actions to render the template, create the repository, and register the resulting Component with Backstage.

### `specs.parameters`

The `parameters` object describes the information to collect from the user and the UI to do so, las [react-jsonschema-form](https://github.com/rjsf-team/react-jsonschema-form).

### `specs.steps`

Outline of the template generation steps:

1. `specs.parameters` are evaluated to produce boolean `enable*` values. (This keeps the logic within the template as simple as possible.)
2. Template is downloaded to a working directory and substitutions performed.
3. Unnecessary files are removed from the working directory based on the `enable*` values.
4. The working directory is then pushed to a GitHub repository.
5. An Azure DevOps pipeline is created for building and deploying the repository.
6. [template/catalog-info.yaml](./src/template/catalog-info.yaml) is updated to include a link to the Azure Devops pipeline.
7. A GitHub Pull Request is created for the `catalog-info.yaml` file that was updated in the previous step.

## `template` folder

The [template](./template/) folder contains templates for C# code and project files. The templating language is [nunjucks](https://mozilla.github.io/nunjucks/), but instead of `{{ var }}` it uses `${{ var }}`.

### General Parameters

General parameters used in the [template code](./template/):

| Parameter       | Type   | Description                            |
| --------------- | ------ | -------------------------------------- |
| name            | string | Base name of the project.              |
| templateName    | string | Name of the template.                  |
| owner           | string | Owner (user or group) of the project.  |
| repoName        | string | Git repository name (Funda.*$name*).   |
| namespacePrefix | string | C# namespace prefix (Funda.*$name*).   |
| fileNamePrefix  | string | File name prefix (Funda.*$name*).      |
| dirNamePrefix   | string | Directory name prefix (Funda.*$name*). |
| applicationName | string | Application name (Funda.*$name*).      |

### Feature flags

Template features are enabled/disabled through the `enable*` flags. A template feature provides any of:

1. Package references in `.csproj` files
2. Global usings
3. Configuration sections in [appsettings.json](./template/src/${{%20values.dirNamePrefix%20}}/appsettings.json)
4. DI setup in [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) and [WebApplicationExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationExtensions.cs)
5. Files with samples/seedwork

| Flag                                                                | Description                                                                                                                    |
| ------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------ |
| [enableFundaMessaging](#enablefundamessaging)                       | Include Funda.Extensions.Messaging support.                                                                                    |
| [enableControllers](#enablecontrollers)                             | Include MVC Controllers support.                                                                                               |
| [enableEndpoints](#enableendpoints)                                 | Include .NET 6 Endpoints support.                                                                                              |
| [enableInMemoryRepository](#enableinmemoryrepository)               | Include a seedwork repository based on in-memory storage.                                                                      |
| [enableSqlServer](#enablesqlserver)                                 | Include [System.Data.SqlClient](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient?view=dotnet-plat-ext-6.0).   |
| [enableSqlServerRepository](#enablesqlserverrepository)             | Include a seedwork repository based on SQL Server.                                                                             |
| [enableCosmosDb](#enablecosmosdb)                                   | Include [Microsoft.Azure.Cosmos](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos?view=dotnet-plat-ext-6.0). |
| [enableCosmosDbRepository](#enablecosmosdbrepository)               | Include a seedwork repository based on CosmosDB.                                                                               |
| [enableEntityFramework](#enableentityframework)                     | Include [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/).                                                    |
| [enableEntityFrameworkInMemory](#enableentityframeworkinmemory)     | Include in-memory database provider for Entity Framework Core.                                                                 |
| [enableEntityFrameworkSqlServer](#enableentityframeworksqlserver)   | Include SQL Server database provider for Entity Framework Core.                                                                |
| [enableEntityFrameworkCosmosDb](#enableentityframeworkcosmosdb)     | Include CosmosDB database provider for Entity Framework Core.                                                                  |
| [enableEntityFrameworkRepository](#enableentityframeworkrepository) | Include a seedwork repository based on Entity Framework.                                                                       |

Note that not every combination of flags makes sense. For example, enabling both [enableEntityFrameworkSqlServer] as well as [enableEntityFrameworkCosmosDb] registers both Sql Server and Cosmos DB database contexts, which is probably not what you want.

#### `enableFundaMessaging`

Adds seedwork for the [Funda.Extensions.Messaging](https://git.funda.nl/projects/PACKAGES/repos/funda.extensions.messaging) package.

|                   | Item                                                                                                                            | Description                               |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------- |
| **Packages**      | [`Funda.{_}`.csproj](./template/src/${{%20values.dirNamePrefix%20}}/${{%20values.fileNamePrefix%20}}.csproj)                         | Funda.Extensions.DateTimeProvider         |
|                   |                                                                                                                                 | Funda.Extensions.Messaging                |
|                   |                                                                                                                                 | Funda.Extensions.Messaging.Metrics        |
|                   |                                                                                                                                 | Funda.Extensions.Messaging.CQRS           |
|                   |                                                                                                                                 | Funda.Extensions.Messaging.InMemory       |
|                   |                                                                                                                                 | Funda.Extensions.Messaging.DatadogTracing |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                               | (*Same as packages*)                      |
| **DI Setup**      | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | `AddFundaMessaging()`                     |
| **Directory**     | [Messaging](./template/src/${{%20values.dirNamePrefix%20}}/Messaging/)                                                          | Sample configuration.                     |

#### `enableControllers`

Provides a sample REST API with `Create`, `FindById` and `Delete` endpoints as an MVC Controller.

MVC Controllers can be selected instead of Endpoints (eg. for compatibility reasons). [Differences between Endpoints and MVC Controllers](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#differences-between-minimal-apis-and-apis-with-controllers). 

|                   | Item                                                                                                                            | Description                                     |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------- |
| **Packages**      | [`Funda.{_}`.csproj](./template/src/${{%20values.dirNamePrefix%20}}/${{%20values.fileNamePrefix%20}}.csproj)                         | Microsoft.AspNetCore.Mvc.Core                   |
|                   |                                                                                                                                 | Microsoft.AspNetCore.Mvc.Versioning             |
|                   |                                                                                                                                 | Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                               | *global using Microsoft.AspNetCore.Mvc;*        |
| **DI Setup**      | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | `AddEndpointsApiExplorer()`                     |
|                   |                                                                                                                                 | `AddVersionedApiExplorer()`                     |
|                   |                                                                                                                                 | `AddApiVersioning()`                            |
|                   |                                                                                                                                 | `AddControllers()`                              |
|                   | [WebApplicationExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs)        | `UseRouting()`                                  |
|                   |                                                                                                                                 | `UseAuthentication()`                           |
|                   |                                                                                                                                 | `UseAuthorization()`                            |
|                   |                                                                                                                                 | `MapControllers()`                              |
| **Directory**     | [Controllers](./template/src/${{%20values.dirNamePrefix%20}}/Controllers/)                                                      | Sample controller.                              |

#### `enableEndpoints`

Provides a sample REST API with `Create`, `FindById` and `Delete` endpoints.

Endpoints are part of the .NET 6 [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0). Endpoints provide a lightweight alternative to MVC Controllers. [Differences between Endpoints and MVC Controllers](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#differences-between-minimal-apis-and-apis-with-controllers). 

|                   | Item                                                                                                                            | Description                 |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------- | --------------------------- |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                               | `Funda.{_}`.Endpoints           |
| **DI Setup**      | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | `AddEndpointsApiExplorer()` |
|                   | [WebApplicationExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs)        | `MappEndpoints()`           |
| **Directory**     | [Endpoints](./template/src/${{%20values.dirNamePrefix%20}}/Endpoints/)                                                          | Sample endpoints.           |

#### `enableInMemoryRepository`

Adds a sample repository that uses a dictionary for in-memory storage. This is the fallback option.

|               | Item                                                                                                                            | Description                              |
| ------------- | ------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------- |
| **DI Setup**  | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | Injects `InMemoryMeasurementRepository`. |
| **Directory** | [Infrastructure/InMemory](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/InMemory/)                              | Sample repository.                       |

#### `enableSqlServer`

Adds `System.Data.SqlClient` as a package reference to use `SqlConnection`, `SqlCommand`, etc.

|                   | Item                                                                                                                                                 | Description                           |
| ----------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------- |
| **Packages**      | [`Funda.{_}`.Infrastructure.csproj](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/${{%20values.fileNamePrefix%20}}.Infrastructure.csproj) | System.Data.SqlClient                 |
| **Configuration** | [appsettings.json](./template/src/${{%20values.dirNamePrefix%20}}/appsettings.json)                                                                  | `SqlServer:ConnectionString`          |
| **Global usings** | [Infrastructure/GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/GlobalUsings.cs)                                      | *global using System.Data.SqlClient;* |

#### `enableSqlServerRepository`

Adds `SqlServerMeasurementRepository` sample repository.

|               | Item                                                                                                                            | Description                               |
| ------------- | ------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------- |
| **DI Setup**  | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | Injects `SqlServerMeasurementRepository`. |
| **Directory** | [Infrastructure/SqlServer](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/SqlServer/)                            | Sample repository.                        |

#### `enableCosmosDb`

Adds `Microsoft.Azure.Cosmos` references.

|                   | Item                                                                                                                                                 | Description                            |
| ----------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------- |
| **Packages**      | [`Funda.{_}`.Infrastructure.csproj](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/${{%20values.fileNamePrefix%20}}.Infrastructure.csproj) | Microsoft.Azure.Cosmos                 |
| **Configuration** | [appsettings.json](./template/src/${{%20values.dirNamePrefix%20}}/appsettings.json)                                                                  | `"CosmosDb:ConnectionString"`          |
|                   |                                                                                                                                                      | `"CosmosDb:DatabaseName"`              |
| **Global usings** | [Infrastructure/GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/GlobalUsings.cs)                                      | *global using Microsoft.Azure.Cosmos;* |

#### `enableCosmosDbRepository`

Adds `CosmosDbMeasurementRepository` sample repository.

|               | Item                                                                                                                            | Description                              |
| ------------- | ------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------- |
| **DI Setup**  | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) | Injects `CosmosDbMeasurementRepository`. |
| **Directory** | [Infrastructure/CosmosDb](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/CosmosDb/)                              | Seedwork repository for Cosmos DB.       |

#### `enableEntityFramework`

Adds support for [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/). 

|                   | Item                                                                                                             | Description                                                  |
| ----------------- | ---------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------ |
| **Packages**      | [`Funda.{_}`.csproj](./template/src/${{%20values.dirNamePrefix%20}}/${{%20values.fileNamePrefix%20}}.csproj)          | Microsoft.EntityFrameworkCore.Design                         |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                | *global using Microsoft.EntityFrameworkCore;*                |
|                   |                                                                                                                  | *global using {{ name }}.Infrastructure.EntityFramework;*    |
|                   | [Infrastructure/GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/GlobalUsings.cs)  | *global using Microsoft.EntityFrameworkCore;*                |
|                   |                                                                                                                  | *global using Microsoft.EntityFrameworkCore.Infrastructure;* |
|                   |                                                                                                                  | *global using {{ name }}.Infrastructure.EntityFramework;*    |
| **Directory**     | [Infrastructure/EntityFramework](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/EntityFramework/) | DbContext infrastructure.                                    |


#### `enableEntityFrameworkSqlServer`

Adds SQL Server as a database provider for Entity Framework Core.

|                   | Item                                                                                                                                  | Description                                                                          |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------ |
| **Packages**      | [`Funda.{_}`.csproj](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/${{%20values.fileNamePrefix%20}}.Infrastructure.csproj) | Microsoft.EntityFrameworkCore.Relational                                             |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                                     | *global using Microsoft.EntityFrameworkCore.SqlServer;*                              |
| **Configuration** | [appsettings.json](./template/src/${{%20values.dirNamePrefix%20}}/appsettings.json)                                                   | `"EntityFramework:SqlServer:ConnectionString"`                                       |
| **DI Setup**      | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs)       | `AddDbContext<IDbContext, SqlServerDbContext>(options => options.UseSqlServer(...))` |
| **Directory**     | [Infrastructure/EntityFramework/SqlServer](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/EntityFramework/SqlServer/)  | SQL Server DbContext.                                                                |


#### `enableEntityFrameworkCosmosDb`

Adds Cosmos DB as a database provider for Entity Framework Core.

|                   | Item                                                                                                                                  | Description                                                                    |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------ |
| **Packages**      | [`Funda.{_}`.csproj](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/${{%20values.fileNamePrefix%20}}.Infrastructure.csproj) | Microsoft.EntityFrameworkCore.Cosmos                                           |
| **Global usings** | [GlobalUsings.cs](./template/src/${{%20values.dirNamePrefix%20}}/GlobalUsings.cs)                                                     | *global using Microsoft.EntityFrameworkCore.Cosmos;*                           |
| **Configuration** | [appsettings.json](./template/src/${{%20values.dirNamePrefix%20}}/appsettings.json)                                                   | `"EntityFramework:CosmosDb:ConnectionString"`                                  |
|                   |                                                                                                                                       | `"EntityFramework:CosmosDb:DatabaseName"`                                      |
| **DI Setup**      | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs)       | `AddDbContext<IDbContext, CosmosDbContext>(options => options.UseCosmos(...))` |
| **Directory**     | [Infrastructure/EntityFramework/CosmosDb](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/EntityFramework/CosmosDb/)    | Cosmos DB DbContext.                                                           |

#### `enableEntityFrameworkRepository`

Adds a sample repository based on [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/). 

|              | Item                                                                                                                                                                                            | Description                                                                 |
| ------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| **DI Setup** | [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs)                                                                 | `AddScoped<IMeasurementRepository, EntityFrameworkMeasurementRepository>()` |
| **File**     | [Infrastructure/EntityFramework/EntityFrameworkMeasurementRepository.cs](./template/src/${{%20values.dirNamePrefix%20}}.Infrastructure/EntityFramework/EntityFrameworkMeasurementRepository.cs) | Sample repository.                                                          |


# Local Development

## Run Backstage locally

For template development it is convenient to run Backstage locally with personal auth details. To do so, grab the Backstage repository:

```shell
$ git clone https://git.funda.nl/scm/tool/backstage.git
```

Then edit `app-config.yaml` in the repo root to add your personal auth details:

```yaml
integrations:
  github:
    - host: github.com
      token: <<YOUR TOKEN>>
  azure:
    - host: dev.azure.com
      token: <<YOUR TOKEN>>
```

It is also convenient to register the template you are developing with Backstage:

```yaml
catalog:
  locations:
    - type: url
      target: <<URL TO TEMPLATE.YAML>>
```

Then start Backstage:

```shell
$ yarn dev
```

If all goes well Backstage should now be running at http://localhost:3000/ and a new component can be created based on the template via http://localhost:3000/create.

## Templating tool

`build.js` is a tool to do nunjucks substitution on the `template` directory. This is faster and more convenient than running the entire Backstage pipeline for every template change. The tool uses the file [template-vars.json](./template-vars.json) as input. 

To run the tool:

```shell
$ npm run build
```

The tool outputs the result in the `dist` directory. **NOTE**: The tool does not remove unnecessary files like the Backstage pipeline.

# Owner

This codebase is owned by **Team Platform**

For questions about this codebase contact **devteam-platform@funda.nl**

[Read more about code ownership](https://git.funda.nl/projects/RFC/repos/platform/browse/0010-teams-will-explicitly-own-their-codebase.MD)

