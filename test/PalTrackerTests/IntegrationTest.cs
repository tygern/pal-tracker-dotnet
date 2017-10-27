using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PalTracker;
using static System.IO.Path;
using static Microsoft.Extensions.PlatformAbstractions.PlatformServices;
using static System.Environment;

namespace PalTrackerTests
{
    public abstract class IntegrationTest
    {
        protected HttpClient TestHttpClient { get; }

        protected virtual IDictionary<string, string> EnvironmentVariables { get; }

        protected IntegrationTest()
        {
            EnvironmentVariables?
                .ToList()
                .ForEach(ev => SetEnvironmentVariable(ev.Key, ev.Value));

            var contentRoot = GetFullPath(
                Combine(Default.Application.ApplicationBasePath, "..", "..", "..", "..", "..", "src", "PalTracker")
            );

            var testServer = new TestServer(
                new WebHostBuilder()
                    .UseContentRoot(contentRoot)
                    .UseStartup<Startup>()
            );

            TestHttpClient = testServer.CreateClient();
        }
    }
}