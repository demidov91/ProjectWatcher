using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectWatcher.Helpers;

namespace ProjectWatcher.Errors
{
    public class NotEnoughRightsException: ProjectWatcherException
    {
        public NotEnoughRightsException()
            :base(ResourcesHelper.GetText("NotEnoughRights", ""))
        {

        }

        public NotEnoughRightsException(String message)
            :base(message)
        {
        }

        
        
    }
}