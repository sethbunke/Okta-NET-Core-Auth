using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeamX.Security.API.Controllers
{
    [Route("[controller]")]
    public class IdentityNoAuthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var headers = Request.Headers.ToList();
            var claims = from c in User.Claims select new { c.Type, c.Value };
            //var output = new { headers, claims };
            return new JsonResult(new { headers, claims, dt = DateTime.Now.ToLongTimeString() });
        }
    }
}