using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// To implement
    /// </summary>
    public class RequestBuilder
    {
        protected String[] variables = new String[0];

        protected String[] functions = new String[0];

        

        /// <summary>
        /// It would be shown only projects of this owner. If "Owner" was not set, all projets should be filtered 
        /// </summary>
        public String Owner
        {
            set { }
        }

        /// <summary>
        /// User-defined filter
        /// </summary>
        public String Filter
        {
            set { }
        }

        /// <summary>
        /// string-defined functions that would be evaluated
        /// </summary>
        public string[] SqlFunctions
        {
            set {
                functions = new String[value.Length];
                value.CopyTo(functions, 0);            
            }
        }

        /// <summary>
        /// Variables that would be evaluated. Only %known_or_unknown_valueName% format
        /// </summary>
        public string[] Variables
        {
            set {
                variables = new String[value.Length];
                value.CopyTo(variables, 0);
            }
        }

        /// <summary>
        /// To implement.
        /// Returns values for each "variable" or "function" in each project that matches "Filter"
        /// </summary>
        /// <returns>Each element of array is collection of requered values in (valueName, value) format</returns>
        public Evaluation[] GetValues()
        {
            Evaluation[] toReturn = new Evaluation[2];
            toReturn[0] = new Evaluation();
            toReturn[1] = new Evaluation();
            int i = 0;
            foreach (String variable in variables)
            {
                toReturn[0].Values.Add(variable, i.ToString());
                i++;
                toReturn[1].Values.Add(variable, i.ToString());
                i++;
            }
            foreach (String function in functions)
            {
                toReturn[0].Formulas.Add(function, i.ToString());
                i++;
                toReturn[1].Formulas.Add(function, i.ToString());
                i++;
            }
            return toReturn.ToArray();
        }
    }
}
