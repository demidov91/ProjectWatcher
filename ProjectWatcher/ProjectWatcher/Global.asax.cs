using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ProjectWatcher
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("ForProjects", "Projects/{action}", new {controller = "Projects", action = "Index"});
            routes.MapRoute("EditProjectProperties", "Project{projectId}/{action}", new {controller="Project", action="Index" });
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Projects", action = "Index" });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Authorization.AuthorizationHelper.LoadUserRoles();
            Helpers.ResourcesHelper.LoadResourses();
            DAL.Interface.Starter.Start();
            SystemSettings.TypeValidationHelper.LoadTypes();
            SystemSettings.DBDefinitionsHelper.Load();
        }

        public void WindowsAuthentication_OnAuthenticate(Object Source, WindowsAuthenticationEventArgs e)
        {
            e.User = new Authorization.RolablePrincipal(e.Identity);
        }
    }
}