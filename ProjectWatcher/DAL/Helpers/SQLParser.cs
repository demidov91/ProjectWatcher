using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace DAL.Helpers
{
    internal static class SQLParser
    {
        public static readonly String SQLFunctionPattern = @"(\w{1,3})\((.*?)\)";

        public static readonly String IfArguementsPattern = @"(.*)(?<![\s\\,])[\s\\,]+\%(\w*?)\%[\s\\,]+\%(\w*?)\%";

        public static readonly String TwoArguementsPattern = @"\%(\w*?)\%[\s\\,]+?\%(\w*?)\%";

        public static readonly String FilterPattern = @"(?<first>\%(?<arg1>\w*?)\%\s*?(?<sign>((\>=)|(\<=)|(<)|(>)|(==)|(!=)))\s*?\%?'?(?![\s'\%])(?<arg2>[^(\|\|)(&&)]+)(?<![\s'\%])'?\%?)\s*(((?<operator>((\|\|)|(&&)))\s*(?<tail>.*))|(\z))";

        internal static String Execute(String formula, Project project)
        {
            Match separatedFunction = Regex.Match(formula, SQLFunctionPattern);
            String functionName = separatedFunction.Groups[1].Value;
            String functionArguments = separatedFunction.Groups[2].Value;
            GroupCollection arguements;
            IComparable arg1;
            IComparable arg2;
            switch (functionName)
            {
                case "if":
                case "IF":
                    arguements = Regex.Match(functionArguments, IfArguementsPattern).Groups;
                    String condition = arguements[1].Value;
                    if (IsSatisfying(project, condition))
                    {
                        return arguements[2].Value;
                    }
                    return arguements[3].Value;
                case "min":
                case "MIN":
                    arguements = Regex.Match(functionArguments, TwoArguementsPattern).Groups;
                    try
                    {
                        arg1 = (IComparable)project.GetTypedValue(arguements[1].Value);
                        arg2 = (IComparable)project.GetTypedValue(arguements[2].Value);
                    }
                    catch (InvalidCastException)
                    {
                        return "";
                    }
                    if (arg1.CompareTo(arg2) < 0)
                    {
                        return arg1.ToString();
                    }
                    else
                    {
                        return arg2.ToString();
                    }
                case "max":
                case "MAX":
                    arguements = Regex.Match(functionArguments, TwoArguementsPattern).Groups;
                    try
                    {
                        arg1 = (IComparable)project.GetTypedValue(arguements[1].Value);
                        arg2 = (IComparable)project.GetTypedValue(arguements[2].Value);
                    }
                    catch (InvalidCastException)
                    {
                        return "";
                    }
                    if (arg1.CompareTo(arg2) > 0)
                    {
                        return arg1.ToString();
                    }
                    else
                    {
                        return arg2.ToString();
                    }
                default:
                    return "";
            }
        }

        /// <summary>
        /// Parses inner string for operations separated by "||" or "&&".
        /// </summary>
        /// <param name="project">Checking project.</param>
        /// <param name="condition">Checking condition for project.</param>
        /// <returns></returns>
        internal static bool IsSatisfying(Project project, String condition)
        {
            Match parsedCondition = Regex.Match(condition, FilterPattern);
            if (!parsedCondition.Success)
            {
                return true;
            }
            String arg1 = parsedCondition.Groups["arg1"].Value;
            String arg2 = parsedCondition.Groups["arg2"].Value;
            String operation = parsedCondition.Groups["operation"].Value;
            String sign = parsedCondition.Groups["sign"].Value;
            String tail = parsedCondition.Groups["tail"].Value;
            if (operation == "||")
            {
                return ResolveFor(project, arg1, sign, arg2) || IsSatisfying(project, tail);
            }
            else
            {
                return ResolveFor(project, arg1, sign, arg2) && IsSatisfying(project, tail);
            }

        }


        private static bool ResolveFor(Project project, String arg1, String sign, String arg2)
        {
            IComparable var1;
            IComparable var2;
            int comparisonResult;
            try
            {

                var1 = (IComparable)project.GetTypedValue(arg1);
                var2 = (IComparable)project.GetTypedValue(arg2);
                comparisonResult = var1.CompareTo(var2);
            }
            catch (InvalidCastException)
            {
                return true;
            }
            catch (ArgumentException)
            {
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
            if (sign.Contains('=') && !sign.Contains('!'))
            {
                if (sign == "<=")
                {
                    if (comparisonResult < 0)
                    {
                        return true;
                    }
                }
                else if (sign == ">=")
                {
                    if (comparisonResult > 0)
                    {
                        return true;
                    }
                }
                if (comparisonResult == 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (sign == "<" && comparisonResult < 0)
                {
                    return true;
                }
                if (sign == ">" && comparisonResult > 0)
                {
                    return true;
                }
                if (sign == "==" && comparisonResult == 0)
                {
                    return true;
                }
                return false;
            }

        }






    }
}
