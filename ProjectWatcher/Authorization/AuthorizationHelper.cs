using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authorization.Properties;

namespace Authorization
{
    public static class AuthorizationHelper
    {
        private static string[] Administrators;
        private static string[] KnownUsers; 

        /// <summary>
        /// Updates array of users for each role. Should be invoked each time configuration settings are changed
        /// </summary>
        public static void LoadUserRoles()
        {
            Settings settings = new Settings();
            Administrators = settings.Administrators.Split(',');
            KnownUsers = settings.KnownUsers.Split(','); 
        }

        /// <summary>
        /// Indicates if "user" is in "role"
        /// </summary>
        /// <param name="user">User with domain prefix</param>
        /// <param name="role">Hardcoded roles</param>
        /// <returns></returns>
        public static bool IsInRole(string user, string role)
        {
            switch (role)
            {
                case "administrator":
                    return Administrators.Contains(user);
                case "knownUser":
                    return KnownUsers.Contains(user);
                default:
                    return false;
            }
        }

       
        

    }
}