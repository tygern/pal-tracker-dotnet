using Microsoft.AspNetCore.Mvc;

namespace PalTracker.Controllers
{
    [Route("env")]
    public class EnvController : Controller
    {
        private readonly CfInfo _cfEnv;

        [HttpGet]
        public CfInfo Get() => _cfEnv;

        public EnvController(CfInfo cfEnv)
        {
            _cfEnv = cfEnv;
        }
    }
}