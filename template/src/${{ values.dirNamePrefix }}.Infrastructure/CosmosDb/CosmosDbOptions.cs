namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public class CosmosDbOptions
{
    /// <summary>
    /// The endpoint of the CosmosDb account
    /// </summary>
    public string Endpoint { get; set; } = null!;

    /// <summary>
    /// The primary key of the cosmosdb account
    /// </summary>
    public string PrimaryKey { get; set; } = null!;

    /// <summary>
    /// Name/Id of the database
    /// </summary>
    public string DatabaseId { get; set; } = null!;

    /// <summary>
    /// When set containers and database are created on first usage.
    ///
    /// This is intended for local development against en emulated cosmos db.
    /// On prod and acc database and containers are created by terraform.
    /// </summary>
    public bool CreateDatabaseAndContainersIfNotExists { get; set; }

}
