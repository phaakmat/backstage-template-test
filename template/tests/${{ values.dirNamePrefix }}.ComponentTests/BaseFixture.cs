namespace ${{ values.namespacePrefix }}.ComponentTests;

public class BaseTest: IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    public readonly TestClient TestClient;
    public readonly string BaseUrl;

    public BaseTest(WebApplicationFactory<Program> factory, string baseUrl = "/api/v1")
    {
        TestClient = new TestClient(factory);
        BaseUrl = baseUrl;
    }

    public void Dispose()
    {
        TestClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
