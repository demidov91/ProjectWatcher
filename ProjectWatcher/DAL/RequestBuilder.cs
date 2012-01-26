using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helpers;

namespace DAL
{
    /// <summary>
    /// Create, set Owner, Filter, execute GetValues. Use object of this filter once for each 'GetValues' call
    /// </summary>
    public class RequestBuilder
    {
        protected String filter = null;

        protected String owner = null;

        protected IEnumerable<Project> projects;

        protected HashSet<String> functions = new HashSet<string>();

        protected HashSet<String> variables = new HashSet<string>();

        public RequestBuilder()
        {
            projects = ConnectionHelper.GetProjects();
        }


        /// <summary>
        /// It would be shown only projects of this owner. If "Owner" was not set, all projets should be filtered 
        /// </summary>
        public String Owner
        {
            get
            {
                return owner;
            }
            set
            {
                if (owner == null)
                {
                    projects = projects.Where(x => x.Owner == value);
                    owner = value;
                }
            }
        }

        /// <summary>
        /// User-defined filter
        /// </summary>
        public String Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (filter == null)
                {
                    projects = projects.Where(x => SQLParser.IsSatisfying(x, value));
                    filter = value;
                }
            }
        }

        /// <summary>
        /// string-defined functions that would be evaluated
        /// </summary>
        public string[] SqlFunctions
        {
            set
            {
                foreach(String function in value)
                {
                    functions.Add(function);
                }
            }
        }

        /// <summary>
        /// Variables that would be evaluated. Only %known_or_unknown_valueName% format
        /// </summary>
        public string[] Variables
        {
            set
            {
                foreach(String variable in value)
                {
                    variables.Add(variable);
                }
            }
        }

        /// <summary>
        /// Returns values for each "variable" or "function" in each project that matches "Filter"
        /// </summary>
        /// <returns>Each element of array is collection of requered values in (valueName, value) format</returns>
        public Evaluation[] GetValues()
        {
            List<Evaluation> evaluations = new List<Evaluation>(projects.Count());
            foreach (Project project in projects)
            {
                Evaluation evaluation = new Evaluation();
                evaluation.Values = EvaluateVariables(project);
                evaluation.Formulas = EvaluateFormulas(project);
                evaluations.Add(evaluation);
            }
            return evaluations.ToArray();
        }


        protected Dictionary<String, String> EvaluateVariables(Project project)
        {
            Dictionary<String, String> toReturn = new Dictionary<string, string>();
            foreach (String variable in variables)
            {
                toReturn.Add(variable, project.GetValue(variable));
            }
            return toReturn;
        }

        protected Dictionary<String, String> EvaluateFormulas(Project project)
        {
            Dictionary<String, String> toReturn = new Dictionary<String, String>();
            foreach (String function in functions)
            {
                toReturn.Add(function, SQLParser.Execute(function, project));
            }
            return toReturn;
        }






    }
}
