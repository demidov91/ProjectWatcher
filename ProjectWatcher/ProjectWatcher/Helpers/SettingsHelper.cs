using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectWatcher.Properties;

namespace ProjectWatcher.Helpers
{
    public static class SettingsHelper
    {
        private static Settings instance = new Settings();

        internal static Settings Instance {
            get { 
                return instance; 
            }            
        }

        public static void Reload()
        {
            instance = new Settings();             
        }
    }
}