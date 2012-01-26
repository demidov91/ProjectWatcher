using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Models.Projects
{
    public class FilterModel
    {
        public String LabelBeforeFilter
        {
            get;
            set;
        }

        public String Filter
        {
            get;
            set;
        }

        public String TableDefinition
        {
            get;
            set;
        }
    }
}