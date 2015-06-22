using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    public class TestController : ApiController
    {
        private static string _theMessage = "Hi Mum";

        [HttpGet]
        [Route("")]
        public string Get()
        {
            return _theMessage;
        }

        [HttpGet]
        [Route("{count:int:min(1)?}")]
        public IEnumerable<string> Get(int count)
        {
            for (var i = 1; i <= count; i++)
            {
                yield return _theMessage;
            }
        }

        [HttpPost]
        [Route("{message}")]
        public IHttpActionResult PostWithDataFromUrl(string message)
        {
            _theMessage = message;
            return Ok(_theMessage);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostWithDataFromBody([FromBody]JObject data)
        {
            JToken message;
            if (data.TryGetValue("message", out message))
            {
                _theMessage = message.Value<string>();
            }
            return Ok(_theMessage);
        }
    }
}
