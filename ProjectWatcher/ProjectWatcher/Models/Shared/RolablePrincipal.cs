using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using Authorization;
using DAL.Interface;

namespace ProjectWatcher.Models.Shared
{
    public class RolablePrincipal : IPrincipal, IUser
    {
        readonly IIdentity identity;

        public IIdentity Identity
        {
            get
            {
                return identity;
            }
        }

        public RolablePrincipal(IIdentity identity)
        {
            this.identity = identity;
        }


        public String Name
        {
            get 
            {
                return identity.Name;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role">Hardcoded name of role. Look into app.config for available roles</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return AuthorizationHelper.IsInRole(Name, role);
        }

    }
}