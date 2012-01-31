using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Project;
using ProjectWatcher.Models.Project.Index;
using DAL.Interface;
using Authorization;
using ProjectWatcher.Errors;

namespace ProjectWatcher.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index(int projectId)
        {
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            ProjectsReader dal = new ProjectsReader();
            IProject project = dal.GetProject(projectId);
            if(project == null)
            {
                return RedirectToAction("BadRequest");
            }
            ProjectWithValuesModel model = new ProjectWithValuesModel(project);
            model.IsEditable = (HttpContext.User.IsInRole("administrator") ||
                                model.Owner == HttpContext.User.Identity.Name)
                                   ? true
                                   : false;
            return View(model);
        }

        public ActionResult AddProperties(int projectId)
        {
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            ProjectsReader dal = new ProjectsReader();
            IProject projectToShow = dal.GetProject(projectId);
            if(!contexter.CanModify(projectToShow))
            {
                return RedirectToAction("NotEnoughRights");
            }
            string culture = contexter.GetCulture();
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
            model.ProjectProperties = ProjectHelper.GetVisibleProperties(dal, model.Id);
            if (model.ProjectProperties == null)
            {
                return RedirectToAction("BadRequest");
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
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            ProjectsReader dal = new ProjectsReader();
            IProject actualProject = dal.GetProject(projectId);
            if (contexter.CanModify(actualProject))
            {
                actualProject.DeleteProperty(systemName);                
            }
            return RedirectToAction("AddProperties", new { projectId = projectId });
        }

        [HttpPost]
        public ActionResult AddExistingProperty(int projectId, string systemName)
        {
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            ProjectsReader dal = new ProjectsReader();
            IProject actualProject = dal.GetProject(projectId);
            if (contexter.CanModify(actualProject))
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


        [HttpGet]
        public ActionResult CreationOfNewProperty(int projectId)
        {
            ViewData["projectId"] = projectId;
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            ViewData["projectId"] = projectId;
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
            return PartialView("CreateProperty", model);
        }





        [HttpPost]
        public ActionResult CreationOfNewPropertyClick(PropertyModel model, int projectId)
        {
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            String result = ProjectHelper.CreateNewProperty(projectId, model, true, contexter);
            if (result != null)
            {
                HttpContext.Response.StatusCode = 404;
                return Json(new { error = result }, JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }
          



        
                        
        [HttpPost]
        public PartialViewResult ChangeImportance(String systemName, int projectId)
        {
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            ProjectsReader dal = new ProjectsReader();
            IProject project = dal.GetProject(projectId);
            if(!contexter.CanModify(project))
            {
                throw new NotEnoughRightsException();
            }
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
                modifier.ModifyImportance(toChange);
                return PartialView("ChangeImportance", new PropertyModel { IsImportant = toChange.Important, ProjectId = toChange.ProjectId, SystemName = toChange.SystemName});
            }
        }

        [HttpGet]
        public PartialViewResult EditValue(Int32 id)
        {
            ProjectsReader reader = new ProjectsReader();
            IValue value = reader.GetValue(id);
            IProject project = reader.GetProject(value.ProjectId);
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            if(!contexter.CanModify(project))
            {
                throw new NotEnoughRightsException();
            }
            ValueModel model = new ValueModel(reader.GetValue(id), true);
            ViewData["culture"] = contexter.GetCulture();
            return PartialView("ValueEdit", model);
        }

       
        public ActionResult AjaxValueOperation(int id, Object Value)
        {
            if(!HttpContext.Request.IsAjaxRequest())
            {
                return Badrequest();
            }
            try
            {
                ValueModel model;
                ProjectsReader dal = new ProjectsReader();
                IValue original = dal.GetValue(id);
                HttpContextWarker contexter = new HttpContextWarker(HttpContext);
                bool canModify = contexter.CanModify(original.GetProject());
                if (contexter.Method.Equals(HttpVerbs.Post))
                {
                    original.SetValue(Value);
                    original.Author = contexter.User.Name;
                    original.Time = DateTime.Now;
                    model = new ValueModel(original, canModify);
                    if(!canModify)
                    {
                        throw new NotEnoughRightsException(ResourcesHelper.GetText("OnNotEnoughForModifyingValue", contexter.GetCulture()));
                    }
                    if(!ProjectHelper.Save(model))
                    {
                        throw new InvalidUserInputException();
                    }
                }
                else
                {
                    model = new ValueModel(original, canModify);
                }
                return PartialView("JustLookValue", model);
            }
            catch(ProjectWatcher.Errors.ProjectWatcherException e)
            {
                return Json(new {error = e.Message}, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Badrequest()
        {
            return RedirectToAction("Index", "Projects");
        }

        public ActionResult NotEnoughRights()
        {
            return RedirectToAction("Badrequest");
        }
    }
}
