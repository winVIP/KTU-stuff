using System;
using System.IdentityModel.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(AutoNuoma.Startup))]

namespace AutoNuoma
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Home/Unauthorized")
            });
        }
    }
}
