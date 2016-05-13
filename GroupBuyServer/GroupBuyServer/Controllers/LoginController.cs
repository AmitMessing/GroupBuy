using System.Web.Http;
using GroupBuyServer.Models;
using GroupBuyServer.Utils;
using Newtonsoft.Json.Linq;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            using (var session = NHibernateHandler.CurrSession)
            {
                var userFromDb = session.QueryOver<User>()
                    .Where(x => x.UserName == user.UserName).And(x => x.Password == user.Password).SingleOrDefault();

                if (userFromDb != null)
                {
                    return Ok(userFromDb);
                }
                return BadRequest("User not exists");
            }
        }
    }
}
