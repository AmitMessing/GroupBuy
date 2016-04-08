using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            string strUserName = jsonLoginDetails["userName"].Value<string>();
            string strPassword = jsonLoginDetails["password"].Value<string>();

            using (ISession session = NHibernateHandler.GetSession)
            {
                var user = session.QueryOver<User>()
                    .Where(x => x.FirstName.Equals(strUserName) && x.Password.Equals(strPassword));

                return this.Ok(user);
            }
        }
    }
}
