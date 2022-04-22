# dotnet-feature-api template

This is a Backstage template for generating a C# [Feature API](https://backstage.funda.io/docs/default/Component/golden-paths-feature-api).

# Usage

To use this template, go to https://backstage.funda.io/create and choose the *.NET 6 Feature API template*.

If it does not show up, then it may need to be registered in the Funda Backstage software catalog. This can be done by running the import wizard at https://backstage.funda.io/catalog-import. When it prompts for a *Repository URL*, enter the URL to the [template.yaml](./template.yaml) file in the repository and follow the wizard.

# Template

The file [template.yaml](./template.yaml) contains a [Backstage template entity](https://backstage.io/docs/features/software-templates/software-templates-index). The template entity describes the parameters and the steps needed to create a new Component.

The [template](./template/) folder contains templates for C# code and project files. The templating language is essentially [nunjucks](https://mozilla.github.io/nunjucks/), but instead of writing `{{ var }}` you write `${{ var }}`.

## Parameters

The following parameters are used in the template:

| Parameter                                                         | Type    | Description                                                                                    |
| ----------------------------------------------------------------- | ------- | ---------------------------------------------------------------------------------------------- |
| name                                                              | string  | The base name of the project.                                                                  |
| owner                                                             | string  | The owner (user or group) of the project.                                                      |
| repoName                                                          | string  | The name of the git repository (Funda.*$name*).                                                |
| namespacePrefix                                                   | string  | The C# namespace prefix (Funda.*$name*).                                                       |
| fileNamePrefix                                                    | string  | The prefix to use in file names (Funda.*$name*).                                               |
| dirNamePrefix                                                     | string  | The prefix to use in directory names (Funda.*$name*).                                          |
| applicationName                                                   | string  | The application name (Funda.*$name*).                                                          |
| [enableFundaMessaging](#enablefundamessaging)                     | boolean | Whether to include Funda.Extensions.Messaging support.                                         |
| [enableControllers](#enablecontrollers)                           | boolean | Whether to include MVC Controllers support.                                                    |
| [enableEndpoints](#enableendpoints)                               | boolean | Whether to include Endpoints support.                                                          |
| [enableSqlServer](#enablesqlserver)                               | boolean | Whether to include SQL support.                                                                |
| [enableCosmosDb](#enablecosmosdb)                                 | boolean | Whether to include CosmosDB support.                                                           |
| [enableEntityFramework](#enableentityframework)                   | boolean | Whether to include Entity Framework Core support.                                              |
| [enableEntityFrameworkSqlServer](#enableentityframeworksqlserver) | boolean | Whether to include SQL Server support for Entity Framework Core.                               |
| [enableEntityFrameworkCosmosDb](#enableentityframeworkcosmosdb)   | boolean | Whether to include CosmosDB support for Entity Framework Core.                                 |
| [enableInMemoryRepository](#enableinmemoryrepository)             | boolean | Whether to include an in-memory repository. Enabled when Entity Framework Core is not enabled. |

## Feature flags

Template features are enabled/disabled through the `enable*` flags. A template feature consists of:
- Package references in `.csproj` files
- Configuration sections in [appsettings.json]
- DI setup in [WebApplicationBuilderExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationBuilderExtensions.cs) and [WebApplicationExtensions.cs](./template/src/${{%20values.dirNamePrefix%20}}/Startup/WebApplicationExtensions.cs)
- Files providing functionality and/or seedwork

### `enableEndpoints`

Endpoints are part of the .NET 6 [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0). Endpoints are more straightforward and lightweight than MVC Controllers. [Differences between Endpoints and MVC Controllers](https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio#differences-between-minimal-apis-and-apis-with-controllers). 

### `enableControllers`

MVC Controllers can be selected instead of Endpoints if that's more practical.

### `enableSqlServer`

Adds `System.Data.SqlClient` as a package reference so it's possible to use `SqlCommand` and `SqlConnection`. Also adds configuration to [appsettings.json](./template/src/${{ values.dirNamePrefix }}/appsettings.json).

### `enableFundaMessaging`

Adds seedwork for the [Funda.Extensions.Messaging](https://git.funda.nl/projects/PACKAGES/repos/funda.extensions.messaging) package.

### `enableInMemoryRepository`

Adds a repository that uses in-memory storage. This flag is set when [enableEntityFramework](#enableentityframework) is `false`.

### `enableCosmosDB`

Adds `Microsoft.Azure.Cosmos` as a package reference. Also adds configuration to [appsettings.json](./template/src/${{ values.dirNamePrefix }}/appsettings.json) .

### `enableEntityFramework`

Adds generic support for [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/). 

### `enableEntityFrameworkSqlServer`

Adds SQL Server as a database provider for Entity Framework Core. See [appsettings.json](./template/src/${{ values.dirNamePrefix }}/appsettings.json) for database connection parameters.

### `enableEntityFrameworkCosmosDb`

Adds Cosmos DB as a database provider for Entity Framework Core. See [appsettings.json](./template/src/${{ values.dirNamePrefix }}/appsettings.json) for database connection parameters.

## Steps

Outline of the template generation steps:

1. User inputs are evaluated to produce boolean `enable*` values. (This keeps the logic within the template as simple as possible.)
2. Template is downloaded to a working directory and substitutions performed.
3. Unnecessary files are removed from the working directory based on the `enable*` values.
4. The working directory is then pushed to a GitHub repository.
5. An Azure DevOps pipeline is created for building and deploying the repository.
6. [template/catalog-info.yaml](./src/template/catalog-info.yaml) is updated to include a link to the Azure Devops pipeline.
7. A GitHub Pull Request is created for the `catalog-info.yaml` file that was updated in the previous step.

## Localhost Development

### Run Backstage locally

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

### Templating tool

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

