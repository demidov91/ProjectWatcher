using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using ProjectWatcher.Models.Project;
using System.Web.Mvc;

namespace ProjectWatcher.Helpers
{
    public static class ProjectHelper
    {
        public static PropertyModel[] GetProperties(ProjectsReader dal, int projectId)
        {
            Property[] propertiesFromDAL = dal.GetPropertiesDefinitions(projectId);
            PropertyModel[] outputProperties = new PropertyModel[propertiesFromDAL.Length];
            for (int i = 0; i < propertiesFromDAL.Length; i++ )
            {
                outputProperties[i] = new PropertyModel(propertiesFromDAL[i]);
            }
            return outputProperties;
        }


        public static PropertyModel[] ExcludeProperties(ProjectsReader dal, PropertyModel[] propertiesFromProject)
        {
            Property[] propertiesFromDAL = dal.GetPropertiesDefinitions();
            PropertyModel[] modeledProperties = new PropertyModel[propertiesFromDAL.Length];
            for (int i = 0; i < propertiesFromDAL.Length; i++)
            {
                modeledProperties[i] = new PropertyModel(propertiesFromDAL[i]);
            }
            return modeledProperties.Except(propertiesFromProject).ToArray();
        }

        public static SelectListItem[] GetAvailableTypesForDropdown()
        {
            SelectListItem[] toReturn = new SelectListItem[]{
                new SelectListItem{ Value="String", Text="String" },
                new SelectListItem{ Value="Select", Text="Select" },
                new SelectListItem{ Value="Multyselect", Text="Multyselect" },
                new SelectListItem{ Value="Number", Text="Number" },
                new SelectListItem{ Value="Percentage", Text="Percentage" }                
            };
            return toReturn;
        }

    }
}