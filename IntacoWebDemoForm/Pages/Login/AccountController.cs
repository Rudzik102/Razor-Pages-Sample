using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace IntacoWebDemoForm.Pages.Persons
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignOut()
        {
            return SignOut(
             new AuthenticationProperties { RedirectUri = "./Persons/Create" },
             CookieAuthenticationDefaults.AuthenticationScheme,
             OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}