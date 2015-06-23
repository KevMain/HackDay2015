using System.Web.Http;

namespace API.Controllers
{
    public class MessageController : ApiController
    {
        private static string _theMessage = "Hi Mum";

        [HttpGet]
        [Route("Message")]
        public IHttpActionResult Get()
        {
            return Ok(_theMessage);
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
