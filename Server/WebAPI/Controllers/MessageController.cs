using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class MessageController : ApiController
    {
        private static readonly Dictionary<int, string> Messages = new Dictionary<int, string> {{1, "Default Value"}};
        
        [HttpGet]
        [Route("Message/{messageId}")]
        public HttpResponseMessage Get(int messageId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Messages[messageId]);
        }

        [HttpPost]
        [Route("Message")]
        public HttpResponseMessage Post(string message)
        {
            var nextId = Messages.Max(m => m.Key) + 1;

            Messages.Add(nextId, message);

            return Request.CreateResponse(HttpStatusCode.Created, Messages[nextId]);
        }
    }
}
