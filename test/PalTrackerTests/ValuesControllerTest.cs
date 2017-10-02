using Microsoft.Extensions.Options;
using PalTracker;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests
{
    public class ValuesControllerTest
    {
        [Fact]
        public void Get()
        {
            var options = Options.Create(new Values
            {
                FirstValue = "testing-one",
                SecondValue = "testing-two",
                ThirdValue = "testing-three"
            });

            var controller = new ValuesController(options);

            Assert.Equal(new[] {"testing-one", "testing-two", "testing-three"}, controller.Get());
        }
    }
}