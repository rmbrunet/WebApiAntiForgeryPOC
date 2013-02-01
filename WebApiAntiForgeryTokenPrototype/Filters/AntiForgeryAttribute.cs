using System;
using System.Linq;
using System.Net.Http; //HttpRequestMessageExtensions
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;

namespace WebApiAntiForgeryTokenPrototype {
    /// <summary>
    /// This attribute must generate the Anti Forgery tokens and send them to the client.
    /// http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/1b78397f32fc#src/System.Web.WebPages/Helpers/AntiForgery.cs
    /// </summary>
    public class AntiForgeryAttribute : System.Web.Http.Filters.ActionFilterAttribute {
        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext) {
            if (actionExecutedContext == null) {
                throw new ArgumentNullException("actionContext");
            }

            string cookieToken, newCookieToken, headerToken;

            
            string tokenKey = AntiForgeryConfig.CookieName;

            CookieHeaderValue cookie = actionExecutedContext.Request.Headers.GetCookies(tokenKey).FirstOrDefault();
            cookieToken = cookie != null ? cookie[tokenKey].Value : null;

            AntiForgery.GetTokens(cookieToken, out newCookieToken, out headerToken);
            if (newCookieToken != null) {
                var newCookie = new CookieHeaderValue(tokenKey, newCookieToken);
                newCookie.HttpOnly = true;
                if (AntiForgeryConfig.RequireSsl) {
                    newCookie.Secure = true;
                }
                actionExecutedContext.Response.Headers.AddCookies( new CookieHeaderValue[] { newCookie });
            }

            actionExecutedContext.Response.Headers.Add(tokenKey, headerToken);

            return;
        }
    }
}