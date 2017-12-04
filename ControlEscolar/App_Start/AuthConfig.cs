using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using ControlEscolar.Models;

namespace ControlEscolar
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Para permitir que los usuarios de este sitio inicien sesión con sus cuentas de otros sitios como, por ejemplo, Microsoft, Facebook y Twitter,
            // es necesario actualizar este sitio. Para obtener más información, visite http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "340177706112497",
                appSecret: "7c5e7f4fc6531603d3350a50a295b102");

            OAuthWebSecurity.RegisterGoogleClient();
            OAuthWebSecurity.RegisterYahooClient();
        }
    }
}
