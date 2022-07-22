using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Personal.IdentityServer6Demo.WebNetFramework.Startup))]

namespace Personal.IdentityServer6Demo.WebNetFramework
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:5001",
                RedirectUri = "https://localhost:44301/",
                PostLogoutRedirectUri = "https://localhost:44301/",
                ClientId = "web-net-framework",
                ClientSecret = "secret",
                ResponseType = OpenIdConnectResponseType.Code,
                Scope = OpenIdConnectScope.OpenId
            });
        }
    }
}
