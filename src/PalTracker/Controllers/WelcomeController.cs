using Microsoft.AspNetCore.Mvc;

namespace PalTracker.Controllers
{
    [Route("/")]
    public class WelcomeController : Controller
    {
        [HttpGet]
        public string SayHello()
        {
            return "hello";
        }
    }
}
