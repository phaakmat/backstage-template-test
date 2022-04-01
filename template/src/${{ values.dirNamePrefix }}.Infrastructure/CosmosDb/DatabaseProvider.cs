using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

interface IDatabaseProvider
{
    Task<Database> GetDatabaseAsync();
}

class DatabaseProvider : IDatabaseProvider
{
    private readonly DbOptions _options;
    private CosmosClient _client;

    public DatabaseProvider(IOptions<DbOptions> optionsAccessor)
    {
        _options = optionsAccessor.Value;
    }

    public async Task<Database> GetDatabaseAsync()
    {
        if (_client == null)
        {
            if (_options.PrimaryKey != null)
            {
                _client = new CosmosClient(_options.Endpoint, _options.PrimaryKey);
            }
            else
            {
                _client = new CosmosClient(_options.Endpoint, new DefaultAzureCredential());
            }
        }

        if (bool.Parse(_options.CreateDatabaseAndContainersIfNotExists))
        {
            var databaseResponse = await _client.CreateDatabaseIfNotExistsAsync(_options.DatabaseId);
            return databaseResponse.Database;
        }

        return _client.GetDatabase(_options.DatabaseId);
    }
}
