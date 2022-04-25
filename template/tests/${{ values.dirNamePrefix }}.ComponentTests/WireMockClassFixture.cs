namespace ${{ values.applicationName }}.ComponentTests;

public class TestClient : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient Client { get; }

    public WireMockServer WireMockServer { get; }

    public TestClient(WebApplicationFactory<Program> factory)
    {
        WireMockServer = WireMockServer.Start();
        var port = WireMockServer.Ports.First();
        var url = $"http://localhost:{port}/";

        Client = factory
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
}
