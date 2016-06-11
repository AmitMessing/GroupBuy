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
        [ActionName("register")]
        public IHttpActionResult Save(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                using (var transaction = session.BeginTransaction())
                {
                    // check if the user name allready exists
                    var userFromDb =
                        session.QueryOver<User>().Where(x => x.FirstName == user.UserName).SingleOrDefault();
                    if (userFromDb != null)
                    {
                        return BadRequest("User allready exists");
                    }

                    user.Id = Guid.NewGuid();
                    user.RegisterDate = DateTime.Now;
                    session.Save(user);
                    transaction.Commit();
                    return Ok(user);
                }
            }
        }
    }
}