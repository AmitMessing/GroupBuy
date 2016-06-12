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
        [HttpGet]
        [ActionName("user")]
        public IHttpActionResult GetUser(Guid id)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var user = session.Get<User>(id);
                return Ok(user);
            }
        }

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