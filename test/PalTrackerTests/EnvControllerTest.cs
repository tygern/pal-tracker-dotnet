using Microsoft.Extensions.Options;
using PalTracker;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests
{
    public class EnvControllerTest
    {
        [Fact]
        public void Get()
        {
            var options = Options.Create(new CfInfo
            {
                PORT = "8080",
                MEMORY_LIMIT = "512M",
                CF_INSTANCE_INDEX = "1",
                CF_INSTANCE_ADDR = "127.0.0.1"
            });

            var env = new EnvController(options).Get();

            Assert.Equal("8080", env.PORT);
            Assert.Equal("512M", env.MEMORY_LIMIT);
            Assert.Equal("1", env.CF_INSTANCE_INDEX);
            Assert.Equal("127.0.0.1", env.CF_INSTANCE_ADDR);
        }
    }
}