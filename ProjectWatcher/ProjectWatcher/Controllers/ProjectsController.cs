using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWatcher.Properties;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Projects;
using Authorization;
using DAL.Interface;
using System.IO;
using SystemSettings;
using System.Text;
using ProjectWatcher.Warkers;
using ProjectWatcher.Models.Shared;

namespace ProjectWatcher.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index(string filter, string tableDefinition)
        {
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            if (filter == null)
            {
                filter = "";
            }
            if (tableDefinition == null)
            {
                tableDefinition = SettingsHelper.Instance.DefaultTableDefinition;
            }
            if (TempData["errorUpload"] != null)
            {
                ViewData["errorMessage"] = TempData["errorUpload"];
            }
            HttpContextWarker context = new HttpContextWarker(HttpContext);
            ViewData["filterModel"] = ProjectsHelper.CreateFilterModel(filter, tableDefinition, context.GetCulture());
            ViewData["tableModel"] = ProjectsHelper.CreateTableModel(filter, tableDefinition, HttpContext);
            ViewData["footerModel"] = ProjectsHelper.CreateFooterModel(filter, tableDefinition, context);
            TempData["exportData"] = ProjectsHelper.CreateExportData((TableModel)ViewData["tableModel"]);
            return View();
        }

        /// <summary>
        /// Sends user CSV file
        /// </summary>
        public ActionResult Export()
        {
            byte[] content = ProjectsHelper.WriteDataToByteArray((String)TempData["exportData"]);
            return new FileContentResult(content, "text/text");
        }

        /// <summary>
        /// Gives input csv-file to the DAL layer
        /// </summary>
        /// <param name="uploadFile">csv-file</param>
        /// <returns>if operation ended successfully</returns>
        public ActionResult Import(HttpPostedFileBase uploadFile, String filter, String tableDefinition)
        {
            HttpContextWarker contexter = new HttpContextWarker(HttpContext);
            String culture = contexter.GetCulture();
            if (uploadFile == null)
            {
                TempData["errorUpload"] = ResourcesHelper.GetText("ErrorWhileUploading", culture);
                return RedirectToAction("Index", new { filter = filter, tableDefinition = tableDefinition });
            }
            CsvParser parser = new CsvParser(uploadFile.InputStream);
            Dictionary<int, Evaluation> newRecords = parser.GetValuesForProjects();
            if(newRecords == null)
            {
                TempData["errorUpload"] = ResourcesHelper.GetText("BadInputCsvFileFormat", contexter.GetCulture());
                return RedirectToAction("Index", new { filter = filter, tableDefinition = tableDefinition });
            }
            List<int> badProjects = new List<int>();
<<<<<<< HEAD
            List<int> badRights = new List<int>();
            Modifier modifier = new Modifier();
            ProjectsReader reader = new ProjectsReader();
            foreach (KeyValuePair<int, Evaluation> project in newRecords)
            {
                IProject modifying = reader.GetProject(project.Key);
                if(!contexter.CanModify(modifying))
                {
                    badRights.Add(modifying.Id);
                    continue;
                }
                if (!modifier.ModifyOrCreate(project.Key, project.Value.Values, (RolablePrincipal)HttpContext.User))
=======
            Modifier dal = new Modifier();
            foreach (KeyValuePair<int, Evaluation> project in newRecords)
            {
                if (!dal.ModifyOrCreate(project.Key, project.Value.Values, (RolablePrincipal)HttpContext.User))
>>>>>>> master
                {
                    badProjects.Add(project.Key);
                }
            }
<<<<<<< HEAD
            if (badProjects.Count > 0 || badRights.Count > 0)
            {
                TempData["errorUpload"] = ProjectsHelper.FormUploadErrorMessage(badProjects, badRights, culture);
=======
            if (badProjects.Count > 0)
            {
                TempData["errorUpload"] = ProjectsHelper.FormUploadErrorMessage(badProjects, culture);
>>>>>>> master
            }
            else 
            {
                TempData["errorUpload"] = ResourcesHelper.GetText("ImportSuccess", culture);
            }
            return RedirectToAction("Index", new { filter = filter, tableDefinition = tableDefinition});
        }

        /// <summary>
        /// Creates new project in DB and opens it
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RedirectableAuthorize("~/Projects/Index", Roles = "administrator")]
        public ActionResult NewProject()
        {
            ProjectsReader dal = new ProjectsReader();
            int newProjectId = dal.CreateNewProject(User.Identity.Name);
            return RedirectToAction("Index", "Project", new { projectId = newProjectId });
        }

        [HttpPost]
        public ActionResult EditFilter(String tableDefinition, FilterModel model)
        {
            return RedirectToAction("Index", new {filter=model.Filter, tableDefinition=tableDefinition }); 
        }

        [HttpPost]
        public ActionResult EditTable(String filter, TableModel model)
        {
            return RedirectToAction("Index", new { filter = filter, tableDefinition = model.TableDefinition });
        }
    }
}
