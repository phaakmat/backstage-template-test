using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public interface ICosmosDbClientService
{
    CosmosClient GetClient();
}

public class CosmosDbClientService : ICosmosDbClientService
{
    private readonly CosmosClient _client;

    public CosmosDbClientService(IOptions<CosmosDbOptions> options)
    {
        this._client = new CosmosClient(options.Value.Endpoint, options.Value.PrimaryKey);
    }

    public CosmosClient GetClient()
    {
        return _client;
    }
}
