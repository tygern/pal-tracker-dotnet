using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PalTracker.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ValueOptions _options;

        public ValuesController(IOptions<ValueOptions> options)
        {
            _options = options.Value;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[]
            {
                _options.FirstValue, 
                _options.SecondValue, 
                _options.ThirdValue
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}