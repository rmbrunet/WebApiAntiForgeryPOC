using System;
using System.Linq;
using System.Net.Http; //HttpRequestMessageExtensions
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace WebApiAntiForgeryTokenPrototype {
    /// <summary>
    /// This attribute must generate the Anti Forgery tokens and send them to the client.
    /// </summary>
    public class GenerateHttpHeaderAntiForgeryAttribute : System.Web.Http.Filters.ActionFilterAttribute {
        static string _tokenKey = AntiForgeryConfig.CookieName;
        static Regex _rx = new Regex("value=\"(?<token>.+)\"", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext) {
            if (actionExecutedContext == null) {
                throw new ArgumentNullException("actionContext");
            }
            var inputElement = AntiForgery.GetHtml().ToString(); //Will generate the cookie, etc...
            string newHeaderToken = _rx.Match(inputElement.ToString()).Groups["token"].Value;
            actionExecutedContext.Response.Headers.Add(_tokenKey, newHeaderToken);
            return;
        }
    }
}