using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Authorization
{
    public class RolablePrincipal : IPrincipal
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role">Hardcoded name of role. Look into app.config for available roles</param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return AuthorizationHelper.IsInRole(identity.Name, role);
        }

    }
}