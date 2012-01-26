using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Helpers
{
    public static class HttpContextHelper
    {
        public static void RedirectToBadRequest(this HttpContextBase context)
        {
            context.RedirectToBadRequest(SettingsHelper.Instance.BadRequestPath);
        }


        public static void RedirectToBadRequest(this HttpContextBase context, String path)
        {
            context.Server.Transfer(path);
            
        }
    }
}