using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using DAL.Helpers;
using Validation;

namespace DAL
{
    /// <summary>
    /// Class for reading properties and projects and for their creation
    /// </summary>
    public class ProjectsReader
    {
        ///to implement
        public Property[] GetPropertiesDefinitions(int projectId)
        {
            Property example = new Property();
            example.Name = "Property1";
            example.SystemName = "prop";
            example.Type = "String";
            Property[] toReturn = new Property[1];
            toReturn[0] = example;
            return toReturn;
        }
        
        /// <summary>
        /// Reads all existing properties in the system.
        /// TODO: Find out what exceptions can be thrown inside of method.
        /// </summary>
        /// <returns>Array of property-entities.</returns>
        /// <exception cref="ConnectionException" />
        public Property[] GetPropertiesDefinitions()
        {
            Property[] toReturn = null;
            try
            {
                using (ISession session = ConnectionHelper.OpenSession())
                {
                    toReturn = session.QueryOver<Property>().List<Property>().ToArray();
                    NHibernateUtil.Initialize(toReturn);
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            return toReturn;
        }

        public Project GetProject(int projectId)
        {
            return new Project(); 
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
        /// <exception cref="BadDisplayTypeException" />
        public void CreateNewProperty(String name, String systemName, String type, String[] availableValues)
        {
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
            creating.Name = name;
            creating.SystemName = systemName;
            creating.Type = type;
            
            if (TypeValidationHelper.IsSelectable(type))
            {
                creating.AvailableValues = availableValues;
            }
            ISession session;
            try
            {
                session = ConnectionHelper.OpenSession();
            }
            catch(Exception e)
            {
                throw new ConnectionException(e);
            }
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(creating);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                session.Close();
                throw new BadSystemNameException(); 
            }
            session.Close(); 
        }

        /// <summary>
        /// Creates new project in DB.
        /// </summary>
        /// <returns>Id of created project.</returns>
        public int CreateNewProject()
        {
            return 1;
        }
    }
}
