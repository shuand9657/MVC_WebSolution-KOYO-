﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ResourceServer.Controller
{
    [Authorize]
    public class ServerController : ApiController
    {
        public IEnumerable<object> Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            return identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}