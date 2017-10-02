using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PalTracker.Controllers
{
    [Route("env")]
    public class EnvController : Controller
    {
        private readonly IOptions<CfInfo> _cfOptions;

        [HttpGet]
        public CfInfo Get() => _cfOptions.Value;

        public EnvController(IOptions<CfInfo> cfOptions)
        {
            _cfOptions = cfOptions;
        }
    }
}