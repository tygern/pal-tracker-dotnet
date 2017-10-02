using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PalTracker;
using Xunit;
using static System.Environment;
using static System.IO.Path;
using static Microsoft.Extensions.PlatformAbstractions.PlatformServices;

namespace PalTrackerTests
{
    [Collection("Integration")]
    public class EnvIntegrationTest
    {
        private readonly string _contentRoot;

        public EnvIntegrationTest()
        {
            _contentRoot = GetFullPath(
                Combine(Default.Application.ApplicationBasePath, "..", "..", "..", "..", "..", "src", "PalTracker")
            );
        }

        [Fact]
        public async Task ReturnsCfEnv()
        {
            SetEnvironmentVariable("PORT", "123");
            SetEnvironmentVariable("MEMORY_LIMIT", "512M");
            SetEnvironmentVariable("CF_INSTANCE_INDEX", "1");
            SetEnvironmentVariable("CF_INSTANCE_ADDR", "127.0.0.1");

            var response = await Client().GetAsync("/env");
            response.EnsureSuccessStatusCode();

            var expectedResponse = @"{""PORT"":""123"",""MEMORY_LIMIT"":""512M"",""CF_INSTANCE_INDEX"":""1"",""CF_INSTANCE_ADDR"":""127.0.0.1""}";
            var actualResponse = await response.Content.ReadAsStringAsync();

            Assert.Equal(expectedResponse, actualResponse);
        }

        private HttpClient Client() => TestServer().CreateClient();

        private TestServer TestServer() => new TestServer(
            new WebHostBuilder()
                .UseContentRoot(_contentRoot)
                .UseStartup<Startup>()
        );
    }
}