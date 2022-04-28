namespace ${{ values.namespacePrefix }}.ComponentTests.Api;

public class CreateTests : BaseTest
{
    public CreateTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("27ed99c0-fded-402e-bd19-2310f9e5972d", 1.23, "Summary")]
    public async Task When_InputValid_Returns_SuccessAndResult(Guid id, double temperatureC, string summary)
    {
        // Arrange
        var client = TestClient.HttpClient;

        // Act
        var response = await client.PostAsJsonAsync(BaseUrl, new 
        {
            MeasurementId = id,
            TemperatureC = temperatureC,
            Summary = summary
        });
        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());

        var item = await response.Content!.ReadFromJsonAsync<Measurement>();

        Assert.NotNull(item);
        Assert.Equal(id, item!.id);
        Assert.Equal(temperatureC, item.temperatureC);
        Assert.Equal(summary, item.summary);
    }

    [Theory]
    [ClassData(typeof(InvalidInputTestDataGenerator))]
    public async Task When_InputInvalid_Returns_BadRequestAndProblem(string id, double temperatureC, string summary, string expectedProblemTitle)
    {
        // Arrange
        var client = TestClient.HttpClient;

        // Act
        var response = await client.PostAsJsonAsync(BaseUrl, new
        {
            MeasurementId = id,
            TemperatureC = temperatureC,
            Summary = summary
        });

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json; charset=utf-8", response.Content?.Headers?.ContentType?.ToString());

        var problem = await response.Content!.ReadFromJsonAsync<ProblemDetails>();
        
        Assert.NotNull(problem);
        Assert.Equal((int)HttpStatusCode.BadRequest, problem!.Status);
        Assert.Equal(expectedProblemTitle, problem.Title);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private record Measurement
    {
        public Guid id { get; set; }
        public double temperatureC { get; set; }
        public string? summary { get; set; }
    }

    private class InvalidInputTestDataGenerator : TheoryData<string, double, string, string>
    {
        public InvalidInputTestDataGenerator()
        {
            // Temperature < -273.15
            Add("27ed99c0-fded-402e-bd19-2310f9e5972d", -600.0, "Summary", "One or more validation errors occurred.");
            // Summary.Length > 2000
            Add("27ed99c0-fded-402e-bd19-2310f9e5972d", 1.0, new string('-', 2001),
                "One or more validation errors occurred.");
        }
    }
}
