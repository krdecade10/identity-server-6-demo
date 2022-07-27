using IdentityModel;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Personal.IdentityServer6Demo.WebNetFramework.Startup))]

namespace Personal.IdentityServer6Demo.WebNetFramework
{
    public class Startup
    {
        private readonly string clientId = "viedoc-web";
        private readonly string clientSecret = "viedoc-secret";
        private readonly string redirectUri = "https://localhost:44301/callback";
        private readonly string authority = "https://localhost:12350/";

        public void Configuration(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //LoginPath = new PathString("/Identity/Account/Login"),
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                ExpireTimeSpan = TimeSpan.FromSeconds(10),
                SlidingExpiration = true,
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                Authority = authority,
                RedirectUri = redirectUri,
                PostLogoutRedirectUri = "https://localhost:44301/signout-callback-odic",
                ClientId = clientId,
                ClientSecret = clientSecret,
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                UsePkce = false,
                UseTokenLifetime = false,
                SaveTokens = true,
                RedeemCode = true,
                Scope = "openid profile email",
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false, // This is a simplification
                    //NameClaimType = JwtClaimTypes.Name,
                }
            });
        }
    }
}
