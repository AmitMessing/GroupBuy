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

namespace GroupBuyServer.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(JObject jsonLoginDetails)
        {
            string strUserName = jsonLoginDetails["userName"].Value<string>();
            string strPassword = jsonLoginDetails["password"].Value<string>();

            return this.Ok(new User());
        }
    }
}
