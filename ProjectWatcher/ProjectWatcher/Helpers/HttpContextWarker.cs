using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace ProjectWatcher.Helpers
{
    public class HttpContextWarker
    {
        protected HttpContextBase context;

        protected static String cultureTemplate = @"\w{2}-\w{2}";

        public HttpContextWarker(HttpContextBase context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets culture from cookies, sets default culture if it wasn't in cookies
        /// </summary>
        /// <returns>Culture in "en-US" format</returns>
        public String GetCulture()
        {
            HttpCookie cookieCulture = context.Request.Cookies["culture"];
            String culture;
            if (cookieCulture == null || !Regex.IsMatch(cookieCulture.ToString(), cultureTemplate))
            {
                culture = SettingsHelper.Instance.DefaultCulture;
                context.Response.Cookies.Add(new HttpCookie("culture", culture));
            }
            else
            {
                culture = cookieCulture.ToString();
            }
            return culture;
        }


        public IPrincipal User
        {
            get { return context.User; }
        }



        internal bool CanModify(DAL.Interface.IProject modifying)
        {
            return modifying != null && context.User.IsInRole("administrator") || context.User.Identity.Name == modifying.GetValue("owner");
        }
    }
}