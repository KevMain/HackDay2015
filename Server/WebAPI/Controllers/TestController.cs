using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage GetTestMessage()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Test Message");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
