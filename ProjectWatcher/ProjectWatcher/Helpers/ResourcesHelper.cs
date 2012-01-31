using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Resources;
using System.Drawing;
using ProjectWatcher.Resources;
using ProjectWatcher.Helpers;

namespace ProjectWatcher.Helpers
{
    public static class ResourcesHelper
    {
        private static Dictionary<String, ResourceManager> localizedResourses;


        
        internal static bool LoadResourses()
        {
            try
            {
                localizedResourses = new Dictionary<String, ResourceManager>();
                localizedResourses["ru-RU"] = RussianText.ResourceManager;
                localizedResourses["en-US"] = EnglishText.ResourceManager;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns message in specified language
        /// </summary>
        /// <param name="name">name of message in resource file</param>
        /// <param name="language">Language in "en-US" format</param>
        public static string GetText(string name, string language)
        {
            ResourceManager specifiedLanguage = GetLocalizedResourses(language);
            String toReturn = specifiedLanguage.GetString(name);
            if (toReturn == null)
            {
                return "Message " + name + " was not declared in resourses";
            }
            return toReturn;
        }

        
        private static ResourceManager GetLocalizedResourses(String culture)
        {
            if (localizedResourses.ContainsKey(culture))
            {
                return localizedResourses[culture];
            }
            else
            {
                return localizedResourses[SettingsHelper.Instance.DefaultCulture];
            }
        }



    }
}