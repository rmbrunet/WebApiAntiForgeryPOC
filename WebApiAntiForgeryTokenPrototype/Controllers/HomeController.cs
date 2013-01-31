using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiAntiForgeryTokenPrototype.Controllers
{

    public class HomeController : ApiController
    {
        [HttpGet]
        [GenerateHttpHeaderAntiForgeryAttribute]
        public HttpResponseMessage Tokens() {
            //Just returning the Antiforgery cookie and header tokens. 
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        
        [HttpPost]
        [HttpHeaderAntiForgeryTokenAttribute]
        [GenerateHttpHeaderAntiForgeryAttribute]
        public HttpResponseMessage Data([FromBody]string data) {
            //At this point the Antiforgery Token has been validated by the filter
            //Do something with the data here
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
