using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using ProjectWatcher.Models.Project;
using System.Web.Mvc;

namespace ProjectWatcher.Helpers
{
    public static class ProjectHelper
    {
        public static PropertyModel[] GetProperties(DAL.Interface.ProjectsReader dal, int projectId)
        {
            IProperty[] propertiesFromDAL = dal.GetPropertiesDefinitions(projectId);
            PropertyModel[] outputProperties = new PropertyModel[propertiesFromDAL.Length];
            for (int i = 0; i < propertiesFromDAL.Length; i++ )
            {
                outputProperties[i] = new PropertyModel(propertiesFromDAL[i]);
            }
            return outputProperties;
        }


        internal static PropertyModel[] GetVisibleProperties(ProjectsReader dal, int projectId)
        {
            IProject project = dal.GetProject(projectId);
            if(project == null)
            {
                return null;
            }
            List<PropertyModel> outputProperties = new List<PropertyModel>();
            foreach (IValue value in project.GetValues())
            {
                if (!value.Visible)
                {
                    continue;
                }
                PropertyModel property = new PropertyModel(value.GetProperty());
                property.IsImportant = value.Important;
                property.ProjectId = projectId;
                outputProperties.Add(property);
            }
            return outputProperties.ToArray();
        }

        public static PropertyModel[] ExcludeProperties(ProjectsReader dal, PropertyModel[] propertiesFromProject)
        {
            IProperty[] propertiesFromDAL = dal.GetPropertiesDefinitions();
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


        internal static string CreateNewProperty(int projectId, PropertyModel model, bool important, HttpContextWarker contexter)
        {
            ProjectsReader dal = new ProjectsReader();
            IProject currentProject = dal.GetProject(projectId);
            String culture = contexter.GetCulture();
            if (!contexter.CanModify(currentProject))
            {
                return ResourcesHelper.GetText("NotEnoughRigts", culture);
            }
            try
            {
                dal.CreateNewProperty(model.Name, model.SystemName, model.Type, model.AvailableValuesAsArray);
            }
            catch (BadSystemNameException)
            {
                return ResourcesHelper.GetText("BadPropertySystemName", culture);
            }
            catch (BadPropertyTypeException)
            {
                return ResourcesHelper.GetText("BadPropertyType", culture);
            }
            catch (ConnectionException)
            {
                return ResourcesHelper.GetText("ConnectionError", culture);
            }
            catch (BadDisplayTypeNameException)
            {
                return ResourcesHelper.GetText("BadDisplayType", culture);
            }
            try
            {
                currentProject.AddProperty(model.SystemName, true, important, contexter.User.Identity.Name);
            }
            catch (ConnectionException e)
            {
                return ResourcesHelper.GetText("ConnectionError", contexter.GetCulture());
            }
            return null;
        }

        
    }
}