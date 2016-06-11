using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GroupBuyServer.Models;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Save(User user)
        {
            return Ok();
        }
    }
}