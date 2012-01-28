using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectWatcher.Helpers;

namespace ProjectWatcher
{
    public static class Starter
    {
        public static bool Start()
        {
            return StringHelper.Load() && ResourcesHelper.LoadResourses();
        }
    }
}