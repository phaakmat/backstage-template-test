//namespace ${{ values.namespacePrefix }}.ComponentTests;
//public class BaseTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
//{
//    protected HttpClient _client;
//    protected WireMockServer _wireMockServer;
//    protected string _token;

//    protected BaseTest(WebApplicationFactory<Startup> factory)
//    {
//        var testContext = new TestContext(factory);
//        _client = testContext.Client;
//        _wireMockServer = testContext.WireMockServer;
//        _token = TestTokenProvider.GenerateTestToken(testContext.BaseUrl);
//    }
//    public void Dispose()
//    {
//        _wireMockServer.ResetMappings();
//    }

//    protected void MockWiremockRequest(string path,
//       string method, Dictionary<string, string> urlParams, int responseStatus, object bodyAsJson = null)
//    {
//        var request = Request.Create().WithPath(path).UsingMethod(method);

//        if (urlParams != null)
//        {
//            foreach (var (key, value) in urlParams)
//            {
//                request.WithParam(key, value);
//            }
//        }

//        var response = Response.Create().WithStatusCode(responseStatus);
//        if (bodyAsJson != null)
//        {
//            response.WithBodyAsJson(bodyAsJson);
//        }

//        _wireMockServer.Given(request).RespondWith(response);
//    }

//    protected HttpRequestMessage BuildAuthenticatedRequest(string relativeUrl, HttpMethod method)
//    {
//        var request = new HttpRequestMessage()
//        {
//            Method = method,
//            RequestUri = new Uri(new Uri(_wireMockServer.Urls[0]), relativeUrl)
//        };

//        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

//        return request;
//    }
//}