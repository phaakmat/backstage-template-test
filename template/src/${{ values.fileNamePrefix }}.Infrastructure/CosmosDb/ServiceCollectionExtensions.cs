using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDbInfrastructure(this IServiceCollection services,
        Action<Options> configure)
    {
        services
            .AddSingleton<IDatabaseProvider, DatabaseProvider>()
            .AddSingleton<IContainerSettings<DeliveryStorageModel>, DeliveryContainerSettings>()
            .AddSingleton<IContainerProvider<DeliveryStorageModel>, ContainerProvider<DeliveryStorageModel>>()
            .AddTransient<IDeliveryRepository, CosmosDeliveryRepository>()
            .Configure(configure)
            .AddSingleton<IValidateOptions<Options>, CosmosDbOptionsValidation>();

        return services;
    }
}

public class CosmosDbOptionsValidation : IValidateOptions<Options>
{
    public ValidateOptionsResult Validate(string name, Options options)
    {
        var optionsIsNotNull = options != null;
        var requiredValues = new[] { options.Endpoint, options.DatabaseId, options.CreateDatabaseAndContainersIfNotExists };
        var requiredFieldsAreNotEmpty = !requiredValues.Any(v => string.IsNullOrWhiteSpace(v));
        var valueIsTypeBool = bool.TryParse(options.CreateDatabaseAndContainersIfNotExists, out var value);

        if (optionsIsNotNull && requiredFieldsAreNotEmpty && valueIsTypeBool)
        {
            return ValidateOptionsResult.Success;
        }

        return ValidateOptionsResult.Fail($"CosmosDbOptions validation failed. Please make sure that the options section is defined in the settings and that endpoint and databaseid values are provided. Endpoint: {options.Endpoint}, DatabaseId: {options.DatabaseId}, CreateDatabaseAndContainersIfNotExists: {options.CreateDatabaseAndContainersIfNotExists}.");
    }
}