using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using ProjectWatcher.Helpers;
using DAL.Interface;
using System.Web.Mvc;


namespace ProjectWatcher.Models.Projects
{
    /// <summary>
    /// Stores column definition and can evaluated it with help of Dictionary of "formula-value" pairs
    /// </summary>
    public class ColumnDefinition
    {
        public string Header
        {
            get;
            set;
        }

        public string Formula
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Evaluates formula
        /// </summary>
        /// <param name="values"></param>
        /// <returns>"Formula" with project values instead of sqlFunctions and %values% in viewable form</returns>
        public String ParseFormula(Evaluation values)
        {
            string evaluatedFormula = Formula;
            foreach (KeyValuePair<String, String> value in values.Formulas)
            {
                evaluatedFormula = evaluatedFormula.Replace("$" + value.Key, value.Value);
                
            }
            foreach (KeyValuePair<String, String> value in values.Values)
            {
                evaluatedFormula = Regex.Replace(evaluatedFormula, "%" + value.Key + "%", value.Value);
            }
            return HtmlHelperHelper.RenderValue(evaluatedFormula, Type);
            
        }


        public override int GetHashCode()
        {
            return Width;
        }

        public override bool Equals(object obj)
        {
            ColumnDefinition input = obj as ColumnDefinition;
            if (input == null)
            {
                return false;
            }
            return (input.Header == Header && input.Formula == Formula && input.Type == Type && input.Width == Width);
        }
    }
}