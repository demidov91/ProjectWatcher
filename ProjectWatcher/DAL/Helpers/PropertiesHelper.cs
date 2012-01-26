using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Helpers
{
    public class PropertiesHelper
    {
        public static String DefaultValue(String systemName)
        {
            Property property = ConnectionHelper.GetProperty(systemName);
            if (property == null)
            {
                return "";
            }
            switch (property.Type)
            {
                case "String":
                    return "";
                case "Date":
                    return DateTime.MinValue.ToString();
                case "Select":
                    return (property.AvailableValues.FirstOrDefault() == null ? null : property.AvailableValues.FirstOrDefault().Value);
                case "Number":
                    return "0";
                case "Percentage":
                    return "0";
                default:
                    return "";                   

            }
            
        }
    }
}
