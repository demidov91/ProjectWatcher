using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Project;
using DAL;
using Authorization;

namespace ProjectWatcher.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index(int projectId)
        {
            return View();         
        }

        public ActionResult AddProperties(int projectId)
        {
            HttpContextHelper cultureProvider = new HttpContextHelper(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["projectProperties"] = ResourcesHelper.GetText("ProjectProperties", culture);
            ViewData["availableProperties"] = ResourcesHelper.GetText("AvailableProperties", culture);
            ViewData["createPropertyTitle"] = ResourcesHelper.GetText("CreateProperty", culture);
            ViewData["backToProjectViewTitle"] = ResourcesHelper.GetText("BackToProjectView", culture);
            ViewData["nameTitle"] = ResourcesHelper.GetText("Name", culture);
            ViewData["systemNameTitle"] = ResourcesHelper.GetText("SystemName", culture);
            ViewData["typeTitle"] = ResourcesHelper.GetText("Type", culture);
            ViewData["removeTitle"] = ResourcesHelper.GetText("Remove", culture);
            ViewData["addTitle"] = ResourcesHelper.GetText("Add", culture);
            ProjectModel model = new ProjectModel();
            model.Id = projectId;
            ProjectsReader dal = new ProjectsReader();
            model.ProjectProperties = ProjectHelper.GetProperties(dal, model.Id);
            PropertyModel[] existingProperties = ProjectHelper.ExcludeProperties(dal, model.ProjectProperties);
            model.OtherProperties = existingProperties;
            return View(model);
        }

        public ActionResult DeleteProperty(int projectId, string systemName)
        {
            ProjectsReader dal = new ProjectsReader();
            DAL.Project actualProject = dal.GetProject(projectId);
            if (User.IsInRole("administrator") || User.Identity.Name == actualProject.Owner)
            {
                actualProject.DeleteProperty(systemName);                
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }

        public ActionResult AddExistingProperty(int projectId, string systemName)
        {
            ProjectsReader dal = new ProjectsReader();
            DAL.Project actualProject = dal.GetProject(projectId);
            if (User.IsInRole("administrator") || User.Identity.Name == actualProject.Owner)
            {
                actualProject.AddProperty(systemName);
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }

        public ActionResult CreationOfNewProperty(int projectId)
        {
            ViewData["projectId"] = projectId;
            HttpContextHelper cultureProvider = new HttpContextHelper(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["nameTitle"] = ResourcesHelper.GetText("Name", culture);
            ViewData["sysNameTitle"] = ResourcesHelper.GetText("SystemName", culture);
            ViewData["typeTitle"] = ResourcesHelper.GetText("Type", culture);
            ViewData["availableValuesTitle"] = ResourcesHelper.GetText("ValuesInEnumeration", culture);            
            PropertyModel model = new PropertyModel();
            model.Name = "";
            model.SystemName = "";
            model.Type = "String";
            model.AvailableValues = "";
            ViewData["availableTypes"] = ProjectHelper.GetAvailableTypesForDropdown();
            return View(model); 
        }

        public ActionResult CreationOfNewPropertyClick(PropertyModel model, int projectId)
        {
            if (Request.Form.AllKeys.Contains("accept.x"))
            {
                HttpContextHelper forCulture = new HttpContextHelper(HttpContext);
                String culture = forCulture.GetCulture();
                String result = CreateNewProperty(projectId, model, culture);
                if (result == null)
                {
                    return RedirectToAction("AddProperties", new { projectId = projectId });
                }
                else
                {
                    TempData["errorMessage"] = result;
                    return RedirectToAction("CreationOfNewProperty");
                }
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }
          



        private string CreateNewProperty(int projectId, PropertyModel model, String culture)
        {
            ProjectsReader dal = new ProjectsReader();
            String result = null;
            try
            {
                dal.CreateNewProperty(model.Name, model.SystemName, model.Type, model.AvailableValues.Split('\n'));
            }
            catch (BadSystemNameException)
            {
                return ResourcesHelper.GetText("BadPropertySystemName", culture);
            }
            catch (BadPropertyTypeException)
            {
                return ResourcesHelper.GetText("BadPropertyType", culture);
            }
            catch (ConnectionException)
            {
                return ResourcesHelper.GetText("ConnectionError", culture);
            }
            catch (BadDisplayTypeNameException)
            {
                return ResourcesHelper.GetText("BadDisplayType", culture); 
            }
            Project currentProject = dal.GetProject(projectId);
            if (currentProject.Owner == User.Identity.Name)
            {
                currentProject.AddProperty(model.SystemName);
                return null;
            }
            return ResourcesHelper.GetText("NotEnoughRights", culture);            
        }

       
            
    }
}
