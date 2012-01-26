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

namespace ProjectWatcher.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index(string filter, string tableDefinition, bool? downloadSucces)
        {
            HttpContextHelper cultureProvider = new HttpContextHelper(HttpContext);
            string culture = cultureProvider.GetCulture();
            if (filter == null)
            {
                filter = "";
            }
            if (tableDefinition == null)
            {
                tableDefinition = SettingsHelper.Instance.DefaultTableDefinition;
            }
            ViewData["filterModel"] = ProjectsHelper.CreateFilterModel(filter, tableDefinition, culture);
            ViewData["tableModel"] = ProjectsHelper.CreateTableModel(filter, tableDefinition, HttpContext);
            ViewData["footerModel"] = ProjectsHelper.CreateFooterModel(filter, tableDefinition, downloadSucces, culture);
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
        [RedirectableAuthorize("~/Projects", Roles="administrator")]
        public ActionResult NewProject()
        {
            ProjectsReader dal = new ProjectsReader();
            int projectId = dal.CreateNewProject();
            return RedirectToAction("Index", new { projectId = projectId });
        }

        public ActionResult EditFilter(String tableDefinition, FilterModel model)
        {
            return RedirectToAction("Index", new {filter=model.Filter, tableDefinition=tableDefinition }); 
        }

        public ActionResult EditTable(String filter, TableModel model)
        {
            return RedirectToAction("Index", new { filter = filter, tableDefinition = model.TableDefinition });
        }
    }
}
