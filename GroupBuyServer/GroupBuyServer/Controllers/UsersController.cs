using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using GroupBuyServer.ViewModels;
using NHibernate.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/users")]
    public class UsersController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Save(User user)
        {
            return Ok();
        }

        [HttpGet]
        [ActionName("user")]
        public IHttpActionResult Product(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var user = session.Get<User>(id);
                return Ok(user);
            }
        }
    }
}