using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Project;
using ProjectWatcher.Models.Project.Index;
using DAL.Interface;
using Authorization;

namespace ProjectWatcher.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index(int projectId)
        {
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["projectName"] = ResourcesHelper.GetText("TheProject1", culture);
            ViewData["lastUser"] = ResourcesHelper.GetText("Admin", culture);
            ProjectsReader dal = new ProjectsReader();
            IProject project = dal.GetProject(projectId);
            ProjectWithValuesModel model = new ProjectWithValuesModel(project);
            model.IsEditable = (HttpContext.User.IsInRole("administrator") ||
                                model.Owner == HttpContext.User.Identity.Name)
                                   ? true
                                   : false;
            return View(model);
        }

        public ActionResult AddProperties(int projectId)
        {
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["projectProperties"] = ResourcesHelper.GetText("ProjectProperties", culture);
            ViewData["availableProperties"] = ResourcesHelper.GetText("AvailableProperties", culture);
            ViewData["createPropertyTitle"] = ResourcesHelper.GetText("CreateProperty", culture);
            ViewData["backToProjectViewTitle"] = ResourcesHelper.GetText("BackToProjectView", culture);
            ViewData["nameTitle"] = ResourcesHelper.GetText("Name", culture);
            ViewData["systemNameTitle"] = ResourcesHelper.GetText("SystemName", culture);
            ViewData["typeTitle"] = ResourcesHelper.GetText("Type", culture);
            ViewData["isImportantTitle"] = ResourcesHelper.GetText("IsImportant", culture);
            ViewData["removeTitle"] = ResourcesHelper.GetText("Remove", culture);
            ViewData["addTitle"] = ResourcesHelper.GetText("Add", culture);
            ProjectModel model = new ProjectModel();
            model.Id = projectId;
            ProjectsReader dal = new ProjectsReader();
            model.ProjectProperties = ProjectHelper.GetVisibleProperties(dal, model.Id);
            
            if (model.ProjectProperties == null)
            {
                return RedirectToAction("Index", "Projects");
            }
            PropertyModel[] existingProperties = ProjectHelper.ExcludeProperties(dal, model.ProjectProperties);
            model.OtherProperties = existingProperties;
            return View(model);
        }

        /// <summary>
        /// Hides property.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="systemName">Systemname of hiding property.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteProperty(int projectId, string systemName)
        {
            ProjectsReader dal = new ProjectsReader();
            IProject actualProject = dal.GetProject(projectId);
            if (actualProject != null && (User.IsInRole("administrator") || User.Identity.Name == actualProject.GetValue("owner").ToString()))
            {
                actualProject.DeleteProperty(systemName);                
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }

        [HttpPost]
        public ActionResult AddExistingProperty(int projectId, string systemName)
        {
            ProjectsReader dal = new ProjectsReader();
            IProject actualProject = dal.GetProject(projectId);
            if (actualProject != null && (User.IsInRole("administrator") || User.Identity.Name == actualProject.GetValue("owner")))
            {
                try
                {
                    actualProject.AddProperty(systemName, true, true, User.Identity.Name);
                }
                catch (ConnectionException e)
                {
                    HttpContextWarker context = new HttpContextWarker(HttpContext);
                    TempData["errorMessage"] = ResourcesHelper.GetText("ConnectionError", context.GetCulture());
                }
                
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }

        public ActionResult CreationOfNewProperty(int projectId)
        {
            ViewData["projectId"] = projectId;
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["nameTitle"] = ResourcesHelper.GetText("Name", culture);
            ViewData["sysNameTitle"] = ResourcesHelper.GetText("SystemName", culture);
            ViewData["typeTitle"] = ResourcesHelper.GetText("Type", culture);
            ViewData["availableValuesTitle"] = ResourcesHelper.GetText("ValuesInEnumeration", culture);
            ViewData["isImportantTitle"] = ResourcesHelper.GetText("IsImportant", culture);
            PropertyModel model = new PropertyModel();
            model.Name = "";
            model.SystemName = "";
            model.Type = "String";
            model.AvailableValues = "";
            ViewData["availableTypes"] = ProjectHelper.GetAvailableTypesForDropdown();
            return View(model); 
        }

        [HttpPost]
        public ActionResult CreationOfNewPropertyClick(PropertyModel model, int projectId)
        {
            if (Request.Form.AllKeys.Contains("accept.x"))
            {
                HttpContextWarker contexter = new HttpContextWarker(HttpContext);
                String result = CreateNewProperty(projectId, model, true, contexter);
                if (result == null)
                {
                    return RedirectToAction("AddProperties", new { projectId = projectId });
                }
                else
                {
                    TempData["errorMessage"] = result;
                    return RedirectToAction("CreationOfNewProperty", new{projectId=projectId});
                }
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }
          



        private string CreateNewProperty(int projectId, PropertyModel model, bool important, HttpContextWarker contexter)
        {
            ProjectsReader dal = new ProjectsReader();
            IProject currentProject = dal.GetProject(projectId);
            String culture = contexter.GetCulture();
            if (currentProject == null || currentProject.GetValue("owner") != contexter.User.Identity.Name && !contexter.User.IsInRole("administrator"))
            {
                return ResourcesHelper.GetText("NotEnoughRigts", culture);
            }
            try
            {
                dal.CreateNewProperty(model.Name, model.SystemName, model.Type, model.AvailableValuesAsArray);
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
            try
            {
                currentProject.AddProperty(model.SystemName, true, important, contexter.User.Identity.Name);
            }
            catch (ConnectionException e)
            {   
                return ResourcesHelper.GetText("ConnectionError", contexter.GetCulture());
            }
            return null;
        }
                        
        

        public PartialViewResult ChangeImportance(String systemName, int projectId)
        {
            ProjectsReader dal = new ProjectsReader();
            IProject project = dal.GetProject(projectId);
            IValue toChange = project.GetValues().FirstOrDefault(x => x.SystemName == systemName);
            if (toChange == null)
            {
                return PartialView("ChangeImportance",
                    new PropertyModel { IsImportant = false, ProjectId = projectId, SystemName = systemName });
            }
            else
            {
                Modifier modifier = new Modifier();
                toChange.Important = !toChange.Important;
                modifier.Modify(toChange);
                return PartialView("ChangeImportance", new PropertyModel { IsImportant = toChange.Important, ProjectId = toChange.ProjectId, SystemName = toChange.SystemName});
            }
        }

    }
}
