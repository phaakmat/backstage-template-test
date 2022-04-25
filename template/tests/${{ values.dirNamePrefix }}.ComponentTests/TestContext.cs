//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Logging;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using WireMock.RequestBuilders;
//using WireMock.ResponseBuilders;
//using WireMock.Server;
//using Xunit;

//namespace SavedListingsAPI.ComponentTests
//{
//    public class TestContext : IClassFixture<WebApplicationFactory<Startup>>
//    {
//        public TestContext(WebApplicationFactory<Startup> factory)
//        {

//            WireMockServer = WireMockServer.Start();
//            var port = WireMockServer.Ports.First();
//            BaseUrl = $"http://localhost:{port}";
//            MockAccounts();

//            Client = factory.WithWebHostBuilder(
//                builder =>
//                {
//                    builder.UseEnvironment("testserver");
//                    builder.ConfigureAppConfiguration((hostingConfig, configurationBuilder) =>
//                    {
//                        configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
//                        {
//                            {"SolrConfig:Base", $"{BaseUrl}/searcher"},
//                            {"userDataFundaWonenUrl", BaseUrl},
//                            { "Authority", BaseUrl},
//                            { "IdentityServer:Authority", BaseUrl},
//                            { "IdentityServer:UserIdentityServerUrl", BaseUrl},
//                            { "IdentityServer:ApiName", TestTokenProvider.Audience}
//                        });
//                    });
//                    builder.ConfigureServices(services =>
//                    {
//                        IdentityModelEventSource.ShowPII = true;
//                    });
//                }).CreateClient(
//                new WebApplicationFactoryClientOptions
//                {
//                    AllowAutoRedirect = false,
//                    HandleCookies = false,
//                });
//        }

//        public HttpClient Client { get; }
//        public WireMockServer WireMockServer { get; }
//        public string BaseUrl { get; }

//        private void MockAccounts()
//        {
//            WireMockServer.Given(
//                    Request.Create()
//                        .WithPath($"/.well-known/openid-configuration")
//                        .UsingGet())
//                .RespondWith(Response.Create()
//                    .WithStatusCode(200)
//                    .WithHeader("Content-Type", "application/json; charset=utf-8")
//                    .WithBody("{\"issuer\":\"{{request.origin}}\",\"jwks_uri\":\"{{request.origin}}/.well-known/openid-configuration/jwks\",\"authorization_endpoint\":\"{{request.origin}}/connect/authorize\",\"token_endpoint\":\"{{request.origin}}/connect/token\",\"userinfo_endpoint\":\"{{request.origin}}/connect/userinfo\",\"end_session_endpoint\":\"{{request.origin}}/connect/endsession\",\"check_session_iframe\":\"{{request.origin}}/connect/checksession\",\"revocation_endpoint\":\"{{request.origin}}/connect/revocation\",\"introspection_endpoint\":\"{{request.origin}}/connect/introspect\",\"device_authorization_endpoint\":\"{{request.origin}}/connect/deviceauthorization\",\"frontchannel_logout_supported\":true,\"frontchannel_logout_session_supported\":true,\"backchannel_logout_supported\":true,\"backchannel_logout_session_supported\":true,\"scopes_supported\":[\"openid\",\"profile\",\"email\",\"funda_basic\",\"kopers_api.wonen\",\"kopers_api.waardecheck_email\",\"makelaars_api.ReadOnly\",\"makelaars_api.ReadWrite\",\"statistics_api\",\"UserDataService.FundaWonen.ReadOnly\",\"UserDataService.FundaWonen.ReadWrite\",\"UserDataService.FundaWonen.MigrationReadOnly\",\"UserDataService.FundaWonen.MigrationReadWrite\",\"UserDataService.FundaInBusiness.ReadOnly\",\"UserDataService.FundaInBusiness.ReadWrite\",\"UserDataService.FundaInBusiness.MigrationReadOnly\",\"UserDataService.FundaInBusiness.MigrationReadWrite\",\"marktverkenner\",\"accounts.makelaarAccount\",\"contracts_api\",\"makelaar-leads_api\",\"funda_media.read\",\"funda_media.write\",\"funda_media.admin\",\"funda_contactsync.read\",\"funda_contactsync.write\",\"supplier_api\",\"login_api\",\"reviews_api.ReadWrite\",\"reviews_api.ReadOnly\",\"oogway_api\",\"saved_listings_api\",\"offline_access\"],\"claims_supported\":[\"sub\",\"name\",\"family_name\",\"given_name\",\"middle_name\",\"nickname\",\"preferred_username\",\"profile\",\"picture\",\"website\",\"gender\",\"birthdate\",\"zoneinfo\",\"locale\",\"updated_at\",\"email_verified\",\"email\",\"funda_user_type\"],\"grant_types_supported\":[\"authorization_code\",\"client_credentials\",\"refresh_token\",\"implicit\",\"password\",\"urn:ietf:params:oauth:grant-type:device_code\"],\"response_types_supported\":[\"code\",\"token\",\"id_token\",\"id_token token\",\"code id_token\",\"code token\",\"code id_token token\"],\"response_modes_supported\":[\"form_post\",\"query\",\"fragment\"],\"token_endpoint_auth_methods_supported\":[\"client_secret_basic\",\"client_secret_post\"],\"id_token_signing_alg_values_supported\":[\"RS256\"],\"subject_types_supported\":[\"public\"],\"code_challenge_methods_supported\":[\"plain\",\"S256\"],\"request_parameter_supported\":true}")
//                    .WithTransformer()
//                    );

//            WireMockServer.Given(
//                    Request.Create()
//                        .WithPath($"/.well-known/openid-configuration/jwks")
//                        .UsingGet())
//                .RespondWith(Response.Create()
//                    .WithStatusCode(200)
//                    .WithHeader("Content-Type", "application/json; charset=utf-8")
//                    .WithBody("{\"keys\":[{\"kty\":\"RSA\",\"use\":\"sig\",\"kid\":\"478E65F318C835A91F0DED95BAE0043D8C5E23A1RS256\",\"x5t\":\"R45l8xjINakfDe2VuuAEPYxeI6E\",\"e\":\"AQAB\",\"n\":\"1tCdhQCIwIk7bCucYDgByg4VC0pNc6uG6f3xD3kefsy/73aOIIYLTNNJBZbAG60bXJCryUMO16YqCEnWUavOTvxdz+A+gO4eHhi2nJmsAl2LICyq+Jwibv51rXlsNV163nlskVxA3kw4qEb7OyM6z5ozy59qN721qhK6Bb9UU4IbAjF8tMfRG9H7QBrC2bE4xJS+D6fhXOknWbUmprpcoW876bxGi539qN8gr6girxAjSad+i3NVfa+BOxfh8Sm1msU1ROqW00NAaDTJ/lNh7O6udcFzdvPZ7o5dkipYB5TwkuTr9t34JeWpU3gS1BGn0hPl7N4kXRzGATI1vDT/hQ==\",\"x5c\":[\"MIIC/jCCAeagAwIBAgIQbEIYcZYfIaZA/D8GPdIq8DANBgkqhkiG9w0BAQsFADAVMRMwEQYDVQQDEwpBdXRoU2FtcGxlMB4XDTE3MDYwNjA3MTE0M1oXDTM5MTIzMTIzNTk1OVowFTETMBEGA1UEAxMKQXV0aFNhbXBsZTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAJzetV9wsXLneUOFDqfsc2CZJImx\u002ByVIL6f2vCCx3QMBEa2vOlhPECfxE0f\u002ByOoWnekSJGhgMniMwtgj4P3KYC4nIW/w/V52sUYn8glxQw1d3bfwELLKsAH2HLC8MoLXX1\u002B3oKCUuxEo5YL\u002B3c0bRcaFyJthHpk0vqnnplmDlVYY6co/BZzRiPPZwpFkWIZuAFhrPBItwuyvFKLKzov4Jnq1q\u002BAjbykHoVy5EdZTs\u002BAZuTfW0lF62ZLCYUvqkQTHDhrdpJMB3uryuI7xsXxvCcy8FvOByUhWqKkvw00GL13QaRu5Uc5Mxnn0FJ/j1TPVzhXjd6ZlWGBxe3qEjduvP2UCAwEAAaNKMEgwRgYDVR0BBD8wPYAQqpV7noEykRHRXQ/IFvaZkaEXMBUxEzARBgNVBAMTCkF1dGhTYW1wbGWCEGxCGHGWHyGmQPw/Bj3SKvAwDQYJKoZIhvcNAQELBQADggEBAE2rDeDdOY8wFeJitTQfelBAXsxjnwDu70HwUINRHhuegzAOX6EF5JhENjtCale6N0y5hY\u002BsLvaXvu0WcAobuxHedrDWPNmqC885j0iOP5UZ7xULjuDH1jdMtshtrsAj//2R87W3XZFEulKibgkCh8C\u002B0KvsbyU65YdHGh9ZzREyp5R1zpE1lSDN5APqXYjl3mvYlXp3Fu0VOzCfhH9ytjERU3N5HO7gP0Jx1C0ExDu/0ThpMAnq8th\u002BTGN55ukOVxC6NghnPUgGyDJkiwAV4flL1YFvz1V8AZXy1XaGQq8fA88nfLdBLjkduD7bmFJIKYpoF17RZRFddrN7uk6o4NU=\"],\"alg\":\"RS256\"}]}"));

//            WireMockServer.Given(
//                    Request.Create()
//                        .WithPath($"/connect/token")
//                        .UsingPost())
//                .RespondWith(Response.Create()
//                    .WithStatusCode(200)
//                    .WithHeader("Content-Type", "application/json; charset=utf-8")
//                    .WithBody("{\"access_token\":\"MTQ0NjJkZmQ5OTM2NDE1ZTZjNGZmZjI3\",\"token_type\":\"bearer\",\"expires_in\":3600,\"refresh_token\":\"IwOGYzYTlmM2YxOTQ5MGE3YmNmMDFkNTVk\",\"scope\":\"create\"}"));
//        }
//    }

//}