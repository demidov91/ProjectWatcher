using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectWatcher.Models.Projects;
using System.Text.RegularExpressions;
using DAL.Interface;
using System.Security.Principal;
using System.IO;
using System.Text;


namespace ProjectWatcher.Helpers
{
    public static class ProjectsHelper
    {
        public static readonly string FunctionPattern = @"\$(\w{1,3}\(.*?\))";
        public static readonly string VariablePattern = @"\%([\w_]*)\%";
        public static readonly string XMLPattern = @"\<.+?\>";


        /// <summary>
        /// Creates all necessary data for Filter.ascx
        /// </summary>
        /// <param name="filter">User-defined filter from request</param>
        /// <param name="culture">Culture in "en-US" format</param>
        /// <returns>Model for Filter.ascx</returns>
        public static FilterModel CreateFilterModel(String filter, String tableDefinition, String culture)
        {
            FilterModel model = new FilterModel();
            model.LabelBeforeFilter = ResourcesHelper.GetText("LabelBeforeFilter", culture);
            model.Filter = filter;
            model.TableDefinition = tableDefinition;
            return model;
        }

        /// <summary>
        /// Creates all necessary data for Table.ascx
        /// </summary>
        /// <param name="tableDefinition">User-defined table view from request</param>
        /// <returns>Model for Table.ascx</returns>
        public static TableModel CreateTableModel(String filter, String tableDefinition, HttpContextBase context)
        {
            TableModel model = new TableModel();
            ColumnDefinition[] columns = ParseForColumns(tableDefinition, context);
            model.Headers = columns.Select(x => x.Header).ToArray();
            model.Width = columns.Select(x => x.Width).ToArray();
            model.TableDefinition = tableDefinition;
            model.Filter = filter;
            model.Projects = GetProjectModels(columns, filter, tableDefinition, context.User);
            return model;
        }

        /// <summary>
        /// Collects all necessary data for Footer.ascx
        /// </summary>
        /// <param name="culture">User language</param>
        /// <returns>Model for Footer.ascx</returns>
        public static FooterModel CreateFooterModel(String filter, String tableDefinition, HttpContextWarker context)
        {
            String culture = context.GetCulture();
            FooterModel model = new FooterModel();
            model.Filter = filter;
            model.TableDefinition = tableDefinition;
            model.AddProjectTitle = ResourcesHelper.GetText("AddProject", culture);
            model.IsAdmin = context.User.IsInRole("administrator");
            model.ExportTitle = ResourcesHelper.GetText("Export", culture);
            model.SubmitUploadTitle = ResourcesHelper.GetText("Upload", culture);
            model.UploadTitle = ResourcesHelper.GetText("Import", culture);
            return model;
        }

        /// <summary>
        /// Creates content of CSV file representing current table.
        /// </summary>
        /// <param name="model">Model for current table.</param>
        /// <returns>Content of file that will be sent on "Export" button click.</returns>
        public static String CreateExportData(TableModel model)
        {
            List<String[]> projects = new List<String[]>();
            foreach (ProjectModel project in model.Projects)
            {
                String[] propertiesForOne = new String[model.Headers.Length];
                for (int i = 0; i < project.Properties.Length; i++)
                {
                    propertiesForOne[i] = MakeForCSV(project.Properties[i]);
                }
                projects.Add(propertiesForOne);                 
            }   
            return FormCSVFile(model.Headers, projects.ToArray());
        }


        private static String MakeForCSV(String input)
        {
            input = Regex.Replace(input, XMLPattern, "");
            input = Regex.Replace(input, "\n", " ");
            input = Regex.Replace(input, ";", "|");
            return input;
        }

        /// <summary>
        /// Writes string content into output stream.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="output"></param>
        public static byte[] WriteDataToByteArray(String content)
        {
            return Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(content)).ToArray();
        }

        /// <summary>
        /// Converts user-defined table view into array of columns definitions 
        /// </summary>
        /// <param name="tableDefinition">User-defined table view</param>
        /// <returns>Array of column definitions</returns>
        private static ColumnDefinition[] ParseForColumns(string tableDefinition, HttpContextBase context)
        {
            string[] columnsFromRequest = tableDefinition.Split('\n');
            List<ColumnDefinition> toReturn = new List<ColumnDefinition>(columnsFromRequest.Length);
            foreach (string columnFromRequest in columnsFromRequest)
            {
                if (!columnFromRequest.Contains(','))
                {
                    continue;
                }
                try
                {
                    toReturn.Add(StringToColumn(columnFromRequest, context));
                }
                catch (ArgumentException e)
                {
                    toReturn.Add(new ColumnDefinition { Header = e.Message, Formula="", Type="String", Width = SettingsHelper.Instance.ErrorcolumnWidth });
                }
            }
            return toReturn.ToArray();
        }

        /// <summary>
        /// For using in "ProjectsHelper.ParseForColumns"
        /// </summary>
        /// <param name="comaSeparatedProperties">One line of definition</param>
        /// <returns>Model for each column</returns>
        /// <exception cref="ArguementException"/>
        private static ColumnDefinition StringToColumn(string comaSeparatedProperties, HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentException();
            }
            ColumnDefinition toReturn = new ColumnDefinition();
            string[] properties = Regex.Split(comaSeparatedProperties, @"(?<!\\),");
            if (properties.Length != 4)
            {
                HttpContextWarker cultureProvider = new HttpContextWarker(context);
                String culture = cultureProvider.GetCulture();            
                String message = ResourcesHelper.GetText("BadColumnDefinition", culture);
                throw new ArgumentException(message);
            }
            for (int i = 0; i < 4; i++)
            {
                properties[i] = Regex.Match(properties[i], @"\A *(.*?) *\z").Groups[1].Value;
            }
            toReturn.Header = properties[0];
            toReturn.Formula = properties[1];
            toReturn.Type = properties[2];
            try
            {
                toReturn.Width = Int32.Parse(properties[3]);
            }
            catch
            {
                HttpContextWarker cultureProvider = new HttpContextWarker(context);
                String culture = cultureProvider.GetCulture();            
                String message = ResourcesHelper.GetText("BadWidthDefinition", culture);
                throw new ArgumentException(message);
            }
            return toReturn;
        }

        /// <summary>
        /// Generates project definitions with actual values of properties defined in "columns"
        /// </summary>
        /// <param name="columns">User-defined table view after "ParseForColumns" method</param>
        /// <returns>Models of required projects</returns>
        private static ProjectModel[] GetProjectModels(ColumnDefinition[] columns, String filter, String tableDefinition, IPrincipal user)
        {
            RequestBuilder dal = GetRequestBuilder(filter, tableDefinition, user);
            Evaluation[] values = dal.GetValues();
            ProjectModel[] projects = SetValuesIntoFormulas(values, columns);
            return projects;
        }

        /// <summary>
        /// Creates and sets up DAL accessor  
        /// </summary>
        /// <param name="filter">User-defined filter</param>
        /// <param name="tableDefinition">User-defined table view</param>
        /// <param name="User">System user</param>
        /// <returns>RequestBuilder which is ready for request</returns>
        private static RequestBuilder GetRequestBuilder(String filter, String tableDefinition, IPrincipal User)
        {
            RequestBuilder builder = new RequestBuilder();
            builder.Filter = filter;
            if(!User.IsInRole("administrator"))
            {
                builder.Owner = User.Identity.Name;
            }
            List<String> functions = new List<String>();
            List<String> variables = new List<String>();
            GetFunctionsAndVariables(tableDefinition, functions, variables);
            builder.SqlFunctions = functions.ToArray();
            builder.Variables = variables.ToArray();
            return builder;
        }

        /// <summary>
        /// Gets at first functions and than variables which are out of functions
        /// </summary>
        /// <param name="tableDefinition">User-defined table view</param>
        /// <param name="functions">List for output</param>
        /// <param name="variables">List for output</param>
        private static void GetFunctionsAndVariables(string tableDefinition, List<String> functions, List<String> variables)
        {
            GetFunctions(tableDefinition, functions);
            tableDefinition = DeleteFunctions(tableDefinition);           
            GetVariables(tableDefinition, variables);                         
        }

        /// <summary>
        /// Generates projects. To create each project it gives values of this project to each column.
        /// </summary>
        /// <param name="valuesForEach">Evaluated SQL functions and variables of projects.</param>
        /// <param name="columns">Program definition of each required column.</param>
        /// <returns>Viewable models for projects.</returns>
        public static ProjectModel[] SetValuesIntoFormulas(Evaluation[] valuesForEach, ColumnDefinition[] columns)
        {
            ProjectModel[] projects = new ProjectModel[valuesForEach.Length];
            for (int i = 0; i < projects.Length; i++)
            {
                ProjectModel project = new ProjectModel();
                project.Properties = new String[columns.Length];
                for(int j = 0; j < columns.Length; j++)
                {
                    project.Properties[j] = columns[j].ParseFormula(valuesForEach[i]);
                }
                projects[i] = project;
            }
            return projects;            
        }

        /// <summary>
        /// Gets sql functions from input string
        /// </summary>
        /// <param name="tableDefinition"></param>
        /// <param name="functions">Functions from "tableDefinition" would be added here</param>
        private static void GetFunctions(String tableDefinition, List<String> functions)
        {
            MatchCollection functionCollection = Regex.Matches(tableDefinition, FunctionPattern);
            foreach (Match match in functionCollection)
            {
                functions.Add(match.Groups[1].Value);
            }
        }

        /// <summary>
        /// Deletes all sql functions from input string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Input string without sql functions</returns>
        private static string DeleteFunctions(string input)
        {
            return Regex.Replace(input, FunctionPattern, "");
        }

        /// <summary>
        /// Gets all substrings which are matches ProjectsHelper.VariablePattern
        /// </summary>
        /// <param name="tableWithNoFunctions"></param>
        /// <param name="variables">Found values would be added here</param>
        private static void GetVariables(String tableWithNoFunctions, List<String> variables)
        {
            MatchCollection variableCollection = Regex.Matches(tableWithNoFunctions, VariablePattern);
            foreach (Match match in variableCollection)
            {
                variables.Add(match.Groups[1].Value);
            }            
        }

        private static String FormCSVFile(String[] headers, String[][] projects)
        {
            StringBuilder writer = new StringBuilder();
            foreach (String header in headers)
            {
                writer.Append(header + ';');
            }
            writer.Append('\n');
            foreach (String[] project in projects)
            {
                if (project == null)
                {
                    continue;
                }
                foreach (String value in project)
                {
                    writer.Append(value + ';');
                }
                writer.Append('\n');
            }
            return writer.ToString(); 
        }

        /// <summary>
        /// Writes message for user about failures while uploading values.
        /// </summary>
        /// <param name="badProjects">Ids of bad formed projects.</param>
        /// <param name="culture">User language.</param>
        /// <returns>Message to show user.</returns>
        internal static String FormUploadErrorMessage(List<int> badProjects, string culture)
        {
            StringBuilder message = new StringBuilder();
            message.Append(ResourcesHelper.GetText("BeginOfErrorInUploadingFiles", culture));
            message.Append('\n');
            foreach (int projectId in badProjects)
            {
                message.Append(projectId);
                message.Append(", ");
            }
            message.Remove(message.Length - 2, 2);
            return message.ToString();
        }
    }
}