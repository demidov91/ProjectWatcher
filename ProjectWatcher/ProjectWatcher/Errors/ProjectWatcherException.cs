using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Errors
{
    public abstract class ProjectWatcherException: Exception
    {
        protected String message;

        public ProjectWatcherException(String message)
        {
            this.message = message;
        }

        protected ProjectWatcherException()
            :this(ProjectWatcher.Helpers.ResourcesHelper.GetText("BaseError", ""))
        {
        }


    }
}