using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Xunit;
using static PalTrackerTests.DbTestSupport;

namespace PalTrackerTests
{
    [Collection("Integration")]
    public class ManagementIntegrationTest : IntegrationTest
    {
        protected override IDictionary<string, string> EnvironmentVariables => new Dictionary<string, string>
        {
            {"VCAP_SERVICES", TestDbVcapJson}
        };

        public ManagementIntegrationTest()
        {
            ExecuteSql("TRUNCATE TABLE time_entries");
        }

        [Fact]
        public void HasHealth()
        {
            var response = TestHttpClient.GetAsync("/health").Result;
            var responseBody = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("UP", responseBody["status"]);
            Assert.Equal("UP", responseBody["diskSpace"]["status"]);
            Assert.Equal("UP", responseBody["timeEntry"]["status"]);
        }
        
        [Fact]
        public void HasInfo()
        {
            var response = TestHttpClient.GetAsync("/info").Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}