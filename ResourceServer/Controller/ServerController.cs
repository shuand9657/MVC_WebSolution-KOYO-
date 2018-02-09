using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ResourceServer.Controller
{
    [Authorize]
    public class ServerController : ApiController
    {
        public string Get()
        {
            return this.User.Identity.Name;
        }
    }
}