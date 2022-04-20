namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDbInfrastructure(this IServiceCollection services,
        Action<CosmosDbOptions> configure)
    {
        services
            .Configure<CosmosDbOptions>(configure)
            .AddSingleton<ICosmosDbClientService, CosmosDbClientService>();

        return services;
    }
}
