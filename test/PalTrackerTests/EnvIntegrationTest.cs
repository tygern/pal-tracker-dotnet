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
                @"{""port"":""123"",""memory_limit"":""512M"",""cf_instance_index"":""1"",""cf_instance_addr"":""127.0.0.1""}";
            var actualResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedResponse, actualResponse);
        }
    }
}