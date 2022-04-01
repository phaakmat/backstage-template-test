using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public interface ICosmosDbContainerService
{
    Container GetContainer();
}

public class CosmosDbContainerService : ICosmosDbContainerService
{
    private readonly Container _container;

    public CosmosDbContainerService(ICosmosDbClientService clientService,
        IOptions<CosmosDbOptions> optionsAccessor)
    {
        var options = optionsAccessor.Value;
        this._container = clientService.GetClient().GetContainer(options.DatabaseId, options.ContainerId);
    }

    public Container GetContainer()
    {
        return _container;
    }
}
