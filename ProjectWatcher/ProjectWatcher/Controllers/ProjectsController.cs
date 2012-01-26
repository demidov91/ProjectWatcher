using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWatcher.Properties;
using ProjectWatcher.Helpers;
using ProjectWatcher.Models.Projects;
using Authorization;
using DAL;
using System.IO;
using SystemSettings;

namespace ProjectWatcher.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index(string filter, string tableDefinition, bool? downloadSucces)
        {
            HttpContextWarker cultureProvider = new HttpContextWarker(HttpContext);
            string culture = cultureProvider.GetCulture();
            if (filter == null)
            {
                filter = "";
            }
            if (tableDefinition == null)
            {
                tableDefinition = SettingsHelper.Instance.DefaultTableDefinition;
            }
            HttpContextWarker context = new HttpContextWarker(HttpContext);
            ViewData["filterModel"] = ProjectsHelper.CreateFilterModel(filter, tableDefinition, culture);
            ViewData["tableModel"] = ProjectsHelper.CreateTableModel(filter, tableDefinition, HttpContext);
            ViewData["footerModel"] = ProjectsHelper.CreateFooterModel(filter, tableDefinition, downloadSucces, context);
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
            if (uploadFile == null)
            {
                return RedirectToAction("Index");
            }
            Modifier writer = new Modifier();
            bool succes = writer.Load(uploadFile.InputStream);
            return RedirectToAction("Index", new { filter = filter, tableDefinition = tableDefinition, downloadSucces = succes});
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
