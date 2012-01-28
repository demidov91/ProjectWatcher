using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using SystemSettings;
using System.Data.Objects.DataClasses;
using System.Data.Common;
using DAL;
using DAL.Interface;

namespace DAL.Helpers
{
    class ConnectionHelper
    {
        
        internal static bool LoadORM()
        {  
            return true;
        }

        /// <summary>
        /// Returns definitions for all known user-properties.
        /// </summary>
        /// <returns>Array of user-defined properties from all projects.</returns>
        internal static Property[] GetProperties()
        {
            List<Property> toReturn = new List<Property>();
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                foreach (Property property in db.Properties)
                {
                    toReturn.Add(property);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                 
            }
            return toReturn.ToArray();
        }

        /// <summary>
        /// Gets project definition by id.
        /// </summary>
        /// <param name="id">Id of the searching project.</param>
        /// <returns>Project with specified id or null.</returns>
        /// <exception cref="ConnectionException" />
        internal static Project GetProject(int id)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                return db.Projects.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }


        /// <summary>
        /// Gets all existing projects.
        /// </summary>
        /// <returns>Entities of all projects.</returns>
        /// <exception cref="ConnectionException" />
        internal static IEnumerable<Project> GetProjects()
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                return db.Projects;
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Creates new records in db for avilable values in selection.
        /// </summary>
        /// <param name="availableValues">Enumeration of available values.</param>
        /// <param name="property">Entity of selectable property.</param>
        internal static bool AddAvailableValues(IEnumerable<string> availableValues, Property property)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                foreach (String availableValue in availableValues)
                {
                    db.AddToAvailableValues(new AvailableValue { Value = availableValue, Property = property.SystemName });
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;                
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Tryes to create specified property.
        /// </summary>
        /// <param name="creating">Property to write to db.</param>
        /// <returns>If saving was successful.</returns>
        internal static bool CreateProperty(Property creating)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                db.AddToProperties(creating);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Creates project with specified owner, time of creation and properties that predefined in Validation settings
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dateTime"></param>
        internal static int CreateProject(string owner, DateTime dateTime)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                Project project = Project.CreateProject(dateTime, null);
                db.AddToProjects(project);
                IEnumerable<String> defaultPropertiesNames = DBDefinitionsHelper.GetDefaultProperties();
                foreach (String name in defaultPropertiesNames)
                {
                    Value defaultProperty = Value.CreateValue(name, project.Id, true, true, owner);
                    if (name == "project_url")
                    {
                        defaultProperty.Value1 = DBDefinitionsHelper.GetProjectUrl(project.Id);
                        defaultProperty.Visible = false;
                    }
                    else if (name == "name")
                    {
                        defaultProperty.Value1 = DBDefinitionsHelper.GetDefaultProjectName();
                    }
                    db.AddToValues(defaultProperty);
                }
                Value ownerEntity = project.Values.First(x => x.SystemName == "owner");
                ownerEntity.Value1 = owner;
                db.Refresh(System.Data.Objects.RefreshMode.ClientWins, ownerEntity);
                return project.Id;
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Marks record for deleting.
        /// </summary>
        /// <param name="toDelete"></param>
        private static bool DeleteWithoutSumitting(EntityObject toDelete, ProjectPropertiesEntities db)
        {
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                db.DeleteObject(toDelete);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Deletes record from db.
        /// </summary>
        /// <param name="toDelete"></param>
        /// <exception cref="ConnectionException" />
        internal static void Delete(EntityObject toDelete)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                db.DeleteObject(toDelete);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toDelete"></param>
        /// <param name="visible"></param>
        /// <exception cref="ConnectionException" />
        internal static void SetVisability(Value toDelete, bool visible)
        {
            toDelete.Visible = visible;
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                db.Refresh(System.Data.Objects.RefreshMode.ClientWins, toDelete);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <param name="systemName"></param>
        /// <returns>Value of project property or what user gave method if project doesn't have such property.</returns>
        /// <exception cref="ConnectionException" />
        internal static string GetValue(Project project, String systemName)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                Value toReturn = project.Values.FirstOrDefault(x => x.SystemName == systemName);
                if (toReturn == null)
                {
                    return systemName;
                }
                else
                {
                    return toReturn.Value1;
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        internal static void AddProperties(string[] properties, Project project, bool visible, bool important, String author)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                foreach (String property in properties)
                {
                    Value toAdd = Value.CreateValue(property, project.Id, visible, important, author);
                    if (toAdd == null)
                    {
                        throw new IllegalDBOperationException(project);
                    }
                    else
                    {
                        db.AddToValues(toAdd);
                    }
                }
                db.SaveChanges();
            }
            catch(IllegalDBOperationException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
                
            
        }

        /// <summary>
        /// Sets available values for specified property instead of existing.
        /// </summary>
        /// <param name="availablevalues"></param>
        /// <param name="property"></param>
        /// <exception cref="ConnectionException" />
        internal static void SetAvailableValues(IEnumerable<String> availablevalues, Property property)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                foreach (AvailableValue value in property.AvailableValues)
                {
                    DeleteWithoutSumitting(value, db);
                }
                foreach (String value in availablevalues)
                {
                    AvailableValue availableValue = AvailableValue.CreateAvailableValue(property.SystemName);
                    db.AddToAvailableValues(availableValue);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }


        internal static Property GetProperty(string systemName)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                return db.Properties.FirstOrDefault(x => x.SystemName == systemName);
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                 
            }
        }

        /// <summary>
        /// Saves old version into History table and modifies existing value.
        /// </summary>
        /// <param name="entity">New values of existing project properties.</param>
        /// <param name="author">Author of modification.</param>
        /// <returns>If operation was done.</returns>
        internal static bool ModifyWithHistory(Value entity)
        {
            bool operationSuccess = false;
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                Value oldVersion = db.Values.FirstOrDefault(x => x.Id == entity.Id);
                if (oldVersion == null)
                {
                    throw new IllegalDBOperationException(entity);
                }
                if (oldVersion.Equals(entity))
                {
                    return true;
                }
                History forOldVersion = new History(oldVersion);
                db.AddToHistories(forOldVersion);
                oldVersion.SetLike(entity);
                db.Refresh(RefreshMode.ClientWins, oldVersion);
                if (entity.Important)
                {
                    Project currentProject = db.Projects.FirstOrDefault(x => x.Id == entity.ProjectId);
                    if (currentProject == null)
                    {
                        throw new IllegalDBOperationException(entity);
                    }
                    currentProject.LastChanged = forOldVersion.Id;
                    db.Refresh(RefreshMode.ClientWins, currentProject);
                }
                db.SaveChanges();
                operationSuccess = true;
            }
            catch(IllegalDBOperationException e)
            {
                ///Write known exception to log
            }
            catch (Exception e)
            {
                ///Write unknown exception to log
            }
            finally
            {
                 
            }
            return operationSuccess;
        }


        /// <summary>
        /// Tryes to push values from this entity into db.
        /// </summary>
        /// <param name="entity">Existing entity.</param>
        /// <returns>If operation was successfull.</returns>
        internal static bool Modify(IEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null if there is no such history entity.</returns>
        /// <exception cref="ConnectionException" />
        internal static IHistory GetHistory(int id)
        {
            ProjectPropertiesEntities db = new ProjectPropertiesEntities();
            if (db.Connection.State != System.Data.ConnectionState.Open)
            {
                db.Connection.Open();
            }
            try
            {
                History history = db.Histories.FirstOrDefault(x => x.Id == id);
                return history;
            }
            catch (Exception e)
            {
                throw new ConnectionException(e);
            }
            
        }
    }
}
