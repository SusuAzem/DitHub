using DitHub;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class DitsControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {

        private readonly HttpClient _client;

        public DitsControllerIntegrationTests(TestingWebAppFactory<Startup> factory) => _client = factory.CreateClient();


        [Fact]
        public async Task ArtistDits_Called_ViewDitList()
        {
            var response = await _client.GetAsync("/Dits/ArtistDits");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("My Dittes", responseString);
        }

    }
}
