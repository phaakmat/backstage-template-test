namespace ${{ values.namespacePrefix }}.ComponentTests.Api;

public class FindByIdTests : BaseTest
{
    public FindByIdTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task When_IdUnknown_Returns_NoContent(string id)
    {
        // Arrange
        var client = TestClient.HttpClient;

        // Act
        var response = await client.GetAsync($"{BaseUrl}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Null(response.Content.Headers.ContentType);
        Assert.True(response.Content.Headers.ContentLength == 0);
    }

    [Theory]
    [InlineData("not-a-valid-guid")]
    public async Task When_IdMalformed_Returns_NotFound(string id)
    {
        // Arrange
        var client = TestClient.HttpClient;

        // Act
        var response = await client.GetAsync($"{BaseUrl}/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Null(response.Content.Headers.ContentType);
        Assert.True(response.Content.Headers.ContentLength == 0);
    }
}
