namespace ${{ values.namespacePrefix }}.ComponentTests;

public class TestClient : IDisposable
{
    public HttpClient HttpClient { get; }

    public WireMockServer WireMockServer { get; }

    public TestClient(WebApplicationFactory<Program> factory)
    {
        WireMockServer = WireMockServer.Start();
        var port = WireMockServer.Ports.First();
        var url = $"http://localhost:{port}/";

        HttpClient = factory
            .WithWebHostBuilder(
                builder =>
                {
                    builder.UseEnvironment("testserver");
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddHttpClient("valueClient", c => { c.BaseAddress = new Uri(url); });
                    });
                })
            .CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    HandleCookies = false
                });
    }

    public void Dispose()
    {
        HttpClient.Dispose();
        WireMockServer.Dispose();
        GC.SuppressFinalize(this);
    }
}
