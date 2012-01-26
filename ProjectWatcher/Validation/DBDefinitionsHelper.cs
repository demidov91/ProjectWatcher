using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemSettings
{
    public static class DBDefinitionsHelper
    {
        private static String[] defaultProperties = new String[0];

        public static bool Load()
        {
            try
            {
                defaultProperties = Properties.Settings.Default.DefaultProperties.Split(',');
                return true;
            }
            catch 
            {
                return false;

            }
        }


        public static IEnumerable<String> GetDefaultProperties()
        {
            String[] toReturn = new String[defaultProperties.Length];
            defaultProperties.CopyTo(toReturn, 0);
            return toReturn;
        }

        public static bool IsDefaultProperty(string systemName)
        {
            return defaultProperties.Contains(systemName);
        }




        public static string GetProjectUrl(int id)
        {
            return "Project" + id.ToString();
        }

        public static string GetDefaultProjectName()
        {
            return "Some projectname";
        }
    }
}
