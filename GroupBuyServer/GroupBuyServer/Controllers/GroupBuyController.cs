using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web.Http;
using GroupBuyServer.Models;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> LoginUser(User loginDetails)
        {
            var user = new User();
            user.FirstName = loginDetails.FirstName;
            user.Email = loginDetails.Email;
            user.Id = loginDetails.Id;
            user.LasstName = loginDetails.LasstName;
            user.Password = loginDetails.Password;
            return Ok(user);
        }
    }
}
