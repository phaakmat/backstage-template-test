namespace ${{ values.namespacePrefix }}.Infrastructure.UnitTests;

public class EntityFrameworkMeasurementRepositoryFixture : IDisposable
{
    private readonly TestDbContext _dbContext;

    public IMeasurementRepository Repository { get; }

    public EntityFrameworkMeasurementRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}")
            .Options;

        _dbContext = new TestDbContext(options);
        Repository = new EntityFrameworkMeasurementRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}
