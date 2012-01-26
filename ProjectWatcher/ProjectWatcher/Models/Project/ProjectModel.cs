using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ProjectWatcher.Models.Project
{
    public class ProjectModel
    {
        public int Id
        {
            get;
            set;
        }

        public PropertyModel[] ProjectProperties
        {
            get;
            set;
        }

        public PropertyModel[] OtherProperties
        {
            get;
            set;
        }
    }
}