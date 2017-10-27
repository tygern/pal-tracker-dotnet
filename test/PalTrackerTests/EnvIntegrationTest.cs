using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PalTrackerTests
{
    [Collection("Integration")]
    public class EnvIntegrationTest : IntegrationTest
    {
        protected override IDictionary<string, string> EnvironmentVariables => new Dictionary<string, string>
        {
            {"PORT", "123"},
            {"MEMORY_LIMIT", "512M"},
            {"CF_INSTANCE_INDEX", "1"},
            {"CF_INSTANCE_ADDR", "127.0.0.1"}
        };

        [Fact]
        public async Task ReturnsCfEnv()
        {
            var response = await TestHttpClient.GetAsync("/env");
            response.EnsureSuccessStatusCode();

            var expectedResponse =
                @"{""port"":""123"",""memoryLimit"":""512M"",""cfInstanceIndex"":""1"",""cfInstanceAddr"":""127.0.0.1""}";
            var actualResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedResponse, actualResponse);
        }
    }
}
