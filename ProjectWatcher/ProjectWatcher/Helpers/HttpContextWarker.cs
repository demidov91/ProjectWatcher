using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Principal;
using System.Web.Mvc;
using ProjectWatcher.Models.Shared;

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


        public RolablePrincipal User
        {
            get 
            {
                return context.User as RolablePrincipal;
            }
        }



        internal bool CanModify(DAL.Interface.IProject modifying)
        {
            return (modifying != null) && (context.User.IsInRole("administrator") || context.User.Identity.Name == modifying.GetValue("owner"));
        }

        public HttpVerbs Method
        {
            get
            {
                HttpVerbs answer = HttpVerbs.Get;
                if(context.Request.HttpMethod == "POST")
                    answer = HttpVerbs.Post;
                else if(context.Request.HttpMethod == "GET")
                    answer = HttpVerbs.Get;
                
                return answer;
            }
        }
    }
}