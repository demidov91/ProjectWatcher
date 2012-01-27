using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Helpers
{
    public static class EntityHelper
    {
        public static String Name(this IValue propetry)
        {
            return propetry.GetProperty().DisplayName;
        }
    }
}