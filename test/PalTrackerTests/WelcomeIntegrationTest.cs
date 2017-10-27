using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PalTrackerTests
{
    [Collection("Integration")]
    public class WelcomeIntegrationTest : IntegrationTest
    {
        protected override IDictionary<string, string> EnvironmentVariables => new Dictionary<string, string>
        {
            {"WELCOME_MESSAGE", "hello from integration test"}
        };

        [Fact]
        public async Task ReturnsMessage()
        {
            var response = await TestHttpClient.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var expectedResponse = "hello from integration test";
            var actualResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedResponse, actualResponse);
        }
    }
}