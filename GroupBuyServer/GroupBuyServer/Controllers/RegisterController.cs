using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using Newtonsoft.Json.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/register")]
    public class RegisterController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                user.Id = Guid.NewGuid();
                session.Save(user);
                return Ok(user);
            }
        }
    }
}