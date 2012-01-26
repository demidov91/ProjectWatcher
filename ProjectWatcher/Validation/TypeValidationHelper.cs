using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SystemSettings
{
    public static class TypeValidationHelper
    {
        private static readonly String SystemNamePattern = @"\A[A-Za-z_]{1,50}\z";

        private static readonly String DisplayNamePattern = @"\A\w[^$]{0,100}(?<=\w)\z";

        private static String[] programTypes;

        private static String[] selectableTypes;

        public static void LoadTypes()
        {
            programTypes = Properties.Settings.Default.AvailableTypes.Split(',');
            selectableTypes = Properties.Settings.Default.SelectableTypes.Split(',');
        }



        public static bool IsValidSystemName(string systemName)
        {
            return Regex.IsMatch(systemName, SystemNamePattern);
        }

        public static bool IsValidType(string type)
        {
            return programTypes.Contains(type);            
        }

        public static bool IsValidDisplayName(string name)
        {
            return Regex.IsMatch(name, DisplayNamePattern);
        }

        public static bool IsValidValue(String value)
        {
            return value != null && value.Length > 0;
        }

        public static bool IsSelectable(string type)
        {
            return selectableTypes.Contains(type);
        }
    }
}
