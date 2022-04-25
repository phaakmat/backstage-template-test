//namespace ${{ values.namespacePrefix }}.ComponentTests;

//public class MobileSavedListingsTests : BaseTest
//{
//    private readonly string _sutUrl = "mobile/v1/savedlistings/{0}?page={1}&pageSize={2}";

//    public MobileSavedListingsTests(WebApplicationFactory<Startup> factory) : base(factory)
//    {
//    }

//    [Fact]
//    public async Task GetSavedKoopListingsWithOneObject_IfThereIsOneObjectSaved()
//    {
//        //given
//        var userId = Guid.NewGuid();
//        var page = 1;
//        var pageSize = 1;
//        var userDataRows = SavedListingsTestData.GetTestData(1);

//        var savedObjectsListResponse = SavedListingsTestData.CreateSavedObjectsListResponse(userDataRows);
//        var searchResponse = SavedListingsTestData.CreateSearchResponseWithMultipleResults(userDataRows);

//        //mock user-data-service
//        MockWiremockRequest($"/api/v2/savedobjects/{userId}", "GET", null, 200, savedObjectsListResponse);

//        //mock solr
//        MockWiremockRequest("/searcher/funda/select", "POST", null, 200, searchResponse);

//        // when
//        var httpRequest = BuildAuthenticatedRequest(string.Format(_sutUrl, userId, page, pageSize), HttpMethod.Get);
//        var response = await _client.SendAsync(httpRequest);

//        // then
//        response.EnsureSuccessStatusCode();

//        var savedListing = DynamicObjectUtils.GetResponseBodyAsDynamicObject(response);

//        var listing = (dynamic) ((List<object>) savedListing.listings).Single();

//        Assert.Equal(1, savedListing.page);
//        Assert.Equal(1, savedListing.pages);
//        Assert.Equal(1, savedListing.numFound);
//        Assert.NotEmpty(savedListing.listings);
//        Assert.True(listing.isKoop);
//    }

//}
