namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public class CosmosDbRepository : IMeasurementRepository
{
    private CosmosDbClientService _clientService;
    private Database _database;
    private Container _container;

    public CosmosDbRepository(CosmosDbClientService clientService, string databaseName, string containerName)
    {
        _clientService = clientService;
        _database = _clientService.GetDatabaseAsync(databaseName).Result;
        _container = _clientService.GetContainerAsync(_database, containerName, new ContainerProperties()).Result;
    }

    public async Task<IMeasurement> CreateAsync(IMeasurement measurement, CancellationToken cancellationToken)
    {
        var result = await _container.CreateItemAsync(measurement, cancellationToken: cancellationToken);
        return result?.Resource;
    }

    public async Task<IMeasurement?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _container.ReadItemAsync<IMeasurement>(id.ToString(), PartitionKey.None, cancellationToken: cancellationToken);
        return result?.Resource;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _container.DeleteItemAsync<IMeasurement>(id.ToString(), PartitionKey.None, cancellationToken: cancellationToken);
        return true;
    }
}