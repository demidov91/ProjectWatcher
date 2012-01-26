using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Authorization
{
    public class RedirectableAuthorizeAttribute: AuthorizeAttribute
    {
        protected string redirectUrl;
        
        public RedirectableAuthorizeAttribute(string redirectUrl)
        {
            this.redirectUrl = redirectUrl; 
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;
            var userName = context.User.Identity.Name;
            string[] roles = Roles.Split(',');
            string[] users = Users.Split(',');
            foreach (string role in roles)
            {
                if (AuthorizationHelper.IsInRole(userName, role))
                    return;
            }
            if(users.Contains(userName))
                return;
            filterContext.Result = new RedirectResult(redirectUrl);
        }
    }
}
