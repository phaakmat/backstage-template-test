using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ${{ values.namespacePrefix }}.Infrastructure.CosmosDb;

public class Options
{
    /// <summary>
    /// The endpoint of the cosmosdb account
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// The primary key of the cosmosdb account
    /// </summary>
    public string PrimaryKey { get; set; }

    /// <summary>
    /// When set containers and database are created on first usage.
    ///
    /// This is intended for local development against en emulated cosmos db.
    /// On prod and acc database and containers are created by terraform.
    /// </summary>
    public string CreateDatabaseAndContainersIfNotExists { get; set; }

    /// <summary>
    /// Name/Id of the database
    /// </summary>
    public string DatabaseId { get; set; }
}
