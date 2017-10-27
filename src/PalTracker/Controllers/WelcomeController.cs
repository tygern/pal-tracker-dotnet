using Microsoft.AspNetCore.Mvc;

namespace PalTracker.Controllers
{
    [Route("/")]
    public class WelcomeController : Controller
    {
        
        private readonly Message _message;
        
        [HttpGet]
        public string SayHello() => _message.WelcomeMessage;

        public WelcomeController(Message message)
        {
            _message = message;
        }
    }
}
