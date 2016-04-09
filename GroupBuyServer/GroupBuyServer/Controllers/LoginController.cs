using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GroupBuyServer.Models;
using Newtonsoft.Json.Linq;
using GroupBuyServer.Utils;
using NHibernate;

namespace GroupBuyServer.Controllers
{
    [RoutePrefix("/GroupBuyServer/api/login/")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(JObject jsonLoginDetails)
        {
            var strUserName = jsonLoginDetails["userName"].Value<string>();
            var strPassword = jsonLoginDetails["password"].Value<string>();

            using (var session = NHibernateHandler.CurrSession)
            {
                User user = session.QueryOver<User>()
                    .Where(x => x.FirstName == strUserName).And(x => x.Password == strPassword).SingleOrDefault();

                return Ok(user);
            }
        }
    }
}
