using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class MessageController : ApiController
    {
        private static string _theMessage = "Hi Mum";

        [HttpGet]
        [Route("Message")]
        public string Get()
        {
            return _theMessage;
        }

        [HttpGet]
        [Route("Message/{count:int:min(1)?}")]
        public IEnumerable<string> Get(int count)
        {
            for (var i = 1; i <= count; i++)
            {
                yield return _theMessage;
            }
        }

        [HttpPost]
        [Route("Message")]
        public IHttpActionResult Post(string message)
        {
            _theMessage = message;
            return Ok(_theMessage);
        }
    }
}
