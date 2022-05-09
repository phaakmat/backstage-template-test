namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public class CosmosDbOptions
{
    /// <summary>
    /// Connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// When set containers and database are created on first usage.
    ///
    /// This is intended for local development against en emulated cosmos db.
    /// On prod and acc database and containers are created by terraform.
    /// </summary>
    public bool CreateDatabaseAndContainersIfNotExists { get; set; }

}
