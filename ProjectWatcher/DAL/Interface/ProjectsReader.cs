using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemSettings;
using DAL.Helpers;

namespace DAL.Interface
{
    /// <summary>
    /// Class for reading properties and projects and for their creation
    /// </summary>
    public class ProjectsReader
    {
        public IProperty[] GetPropertiesDefinitions(int projectId)
        {
            Project project = ConnectionHelper.GetProject(projectId);
            if (project == null)
            {
                return null;
            }
            return project.Values.Select(x => x.Property).ToArray();            
        }

        /// <summary>
        /// Gets definitions for visible properties of the project.
        /// </summary>
        /// <param name="projectId">Id of the project to get properties from.</param>
        /// <returns>Array of definitions of visible properties.</returns>
        public IProperty[] GetVisiblePropertiesDefinitions(int projectId)
        {
            Project project = ConnectionHelper.GetProject(projectId);
            if (project == null)
            {
                return null;
            }
            return project.Values.Where(x => x.Visible).Select(x => x.Property).ToArray();
        }
        
        /// <summary>
        /// Reads all existing properties in the system.
        /// </summary>
        /// <returns>Array of property-entities.</returns>
        /// <exception cref="ConnectionException" />
        public IProperty[] GetPropertiesDefinitions()
        {
            return ConnectionHelper.GetProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId">Id of searching project entity.</param>
        /// <returns>Entity interface or null if there is no projects with such id.</returns>
        public IProject GetProject(int projectId)
        {
            try
            {
                return ConnectionHelper.GetProject(projectId);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Creates new unassigned property in database.
        /// </summary>
        /// <param name="name">Should be correct.</param>
        /// <param name="systemName">Should be latin singleline word.</param>
        /// <param name="type">Only types defined in this program.</param>
        /// <param name="availableValues">Only for types with selection.</param>
        /// <exception cref="ConnectionException" />
        /// <exception cref="BadSystemNameException" />
        /// <exception cref="BadPropertyTypeException" />
        /// <exception cref="BadDisplayTypeNameException" />
        public void CreateNewProperty(String name, String systemName, String type, String[] availableValues)
        {
            if(name == null)
            {
                throw new BadSystemNameException();
            }
            name = name.CutWhitespaces();
            systemName = systemName.CutWhitespaces();
            if(!TypeValidationHelper.IsValidSystemName(systemName))
            {
                throw new BadSystemNameException();
            }

            if(!TypeValidationHelper.IsValidType(type))
            {
                throw new BadPropertyTypeException();
            }

            if(!TypeValidationHelper.IsValidDisplayName(name))
            {
                throw new BadDisplayTypeNameException();
            }
            Property creating = new Property();
            creating.DisplayName = name;
            creating.SystemName = systemName;
            creating.Type = type;

            if (!ConnectionHelper.CreateProperty(creating))
            {
                throw new BadSystemNameException();
            }
            if (TypeValidationHelper.IsSelectable(type))
            {                
                ConnectionHelper.AddAvailableValues(availableValues.Where(x => TypeValidationHelper.IsValidValue(x)), creating);
            }
            
        }

        /// <summary>
        /// Creates new project in DB.
        /// </summary>
        /// <param name="owner">Name of owner of new project.</param>
        /// <returns>Id of created project.</returns>
        public int CreateNewProject(String owner)
        {
            return ConnectionHelper.CreateProject(owner);
        }

        /// <summary>
        /// Gives you entity of value from db by Id
        /// </summary>
        /// <param name="valueId"></param>
        /// <returns></returns>
        public IValue GetValue(int valueId)
        {
            try
            {
                return ConnectionHelper.GetValue(valueId);
            }
            catch (ConnectionException e)
            {
                return null;
                
            }
        }
    }
}
