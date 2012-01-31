using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using DAL.Interface;

namespace DAL.Helpers
{
    public static class EntityHelper
    {
        /// <summary>
        /// Converts value that gave us user into this database format. 
        /// </summary>
        /// <returns>Good format of value.</returns>
        public static Object FormatForDB(IValue entityToSave)
        {
            return FormatForDB(entityToSave.GetValue(), entityToSave.GetProperty());
        }


        /// <summary>
        /// Converts value that gave us user into this database format. 
        /// </summary>
        /// <returns>Good format of value.</returns>
        public static String FormatForDB(Object valueToSave, IProperty propertyOfvalue)
        {
            String type = propertyOfvalue.Type;
            switch (type)
            {
                case "String":
                    if (valueToSave as Array != null)
                    {
                        return String.Concat(((String[])valueToSave).Where(x => x.Length > 0).Select(x => x + '\n')).ToString();
                    }
                    if (valueToSave as String != null)
                    {
                        return valueToSave.ToString();
                    }
                    return "";
                    break;
                case "Number":
                    if (valueToSave as Array != null)
                    {
                        return ((String[])valueToSave)[0].ToString();
                    }
                    if (valueToSave as String != null)
                    {
                        return valueToSave.ToString();
                    }
                    return "0";
                    break;
                case "Percentage":
                    if (valueToSave as Array != null)
                    {
                        return ((String[])valueToSave)[0].ToString();
                    }
                    if (valueToSave as String != null)
                    {
                        return valueToSave.ToString();
                    }
                    return "0";
                    break;
                default:
                    return valueToSave.ToString();
            }
            

        }




        /// <summary>
        /// Returns value as object of type set in db or string "systemName" converted into most relevant type if this is not realy name of property.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="systemName"></param>
        /// <returns>Int32 for numbers and percentage, IEnumerable of string for multyselect, null in case of convertation exception. Int32, Boolean, Date or String for not a property.</returns>
        public static Object GetTypedValue(this Project project, String systemName)
        {
            Property property = ConnectionHelper.GetProperty(systemName);
            if (property == null)
            {
                int intResult;
                if (Int32.TryParse(systemName, out intResult))
                {
                    return intResult;
                }
                DateTime dateResult = new DateTime();
                if (DateTime.TryParse(systemName, out dateResult))
                {
                    return dateResult;
                }
                bool boolResult;
                if (Boolean.TryParse(systemName, out boolResult))
                {
                    return boolResult;
                }
                return systemName;
            }
            String type = property.Type;
            switch (type)
            {
                case "Number":
                case "Percentage":
                    try
                    {
                        return Int32.Parse(project.GetValue(systemName));
                    }
                    catch (FormatException)
                    {
                        return null;
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }
                case "Multyselect":
                    return project.GetValue(systemName).Split('\r', '\n').Where(x => SystemSettings.TypeValidationHelper.IsValidValue(x));
                default:
                    return project.GetValue(systemName);
            }
        }
    }
}
