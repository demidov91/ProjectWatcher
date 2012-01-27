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
        
        private static ProjectPropertiesEntities db;

        internal static bool LoadORM()
        {
            try
            {    
                db = new ProjectPropertiesEntities();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns definitions for all known user-properties.
        /// </summary>
        /// <returns>Array of user-defined properties from all projects.</returns>
        internal static Property[] GetProperties()
        {
            List<Property> toReturn = new List<Property>();
            foreach (Property property in db.Properties)
            {
                toReturn.Add(property);
            }
            return toReturn.ToArray();
        }

        /// <summary>
        /// Gets project definition by id.
        /// </summary>
        /// <param name="id">Id of the searching project.</param>
        /// <returns>Project with specified id.</returns>
        internal static Project GetProject(int id)
        {            
            return db.Projects.FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// Gets all existing projects.
        /// </summary>
        /// <returns>Entities of all projects.</returns>
        internal static IEnumerable<Project> GetProjects()
        {
            return db.Projects;
        }

        /// <summary>
        /// Creates new records in db for avilable values in selection.
        /// </summary>
        /// <param name="availableValues">Enumeration of available values.</param>
        /// <param name="property">Entity of selectable property.</param>
        internal static void AddAvailableValues(IEnumerable<string> availableValues, Property property)
        {
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    foreach (String availableValue in availableValues)
                    {
                        db.AddToAvailableValues(new AvailableValue { Value = availableValue, Property = property.SystemName });
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            finally 
            {
                db.Connection.Close();
            }
        }

        /// <summary>
        /// Tryes to create specified property.
        /// </summary>
        /// <param name="creating">Property to write to db.</param>
        /// <returns>If saving was successful.</returns>
        internal static bool CreateProperty(Property creating)
        {
            Boolean resultOfOperation = false;
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    db.AddToProperties(creating);
                    db.SaveChanges();
                    transaction.Commit();
                }
                resultOfOperation = true;
            }
            catch (Exception)
            {
                resultOfOperation = false;
            }
            finally
            {
                db.Connection.Close();
            }
            return resultOfOperation;
        }

        /// <summary>
        /// Creates project with specified owner, time of creation and properties that predefined in Validation settings
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dateTime"></param>
        internal static int CreateProject(string owner, DateTime dateTime)
        {
            Project project = Project.CreateProject(dateTime, dateTime);
            try
            {
                db.Connection.Open();
                System.Data.Common.DbTransaction transaction = db.Connection.BeginTransaction();
                db.AddToProjects(project);
                db.SaveChanges();
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
                db.SaveChanges();
                Value ownerEntity = project.Values.First(x => x.SystemName == "owner");
                ownerEntity.Value1 = owner;
                db.Refresh(System.Data.Objects.RefreshMode.ClientWins, ownerEntity);
                db.SaveChanges();
                transaction.Commit();
                db.Connection.Close();
            }
            catch (Exception e)
            {
                db.Connection.Close();
                throw new ConnectionException(e);
            }            
            return project.Id;
        }

        /// <summary>
        /// Marks record for deleting.
        /// </summary>
        /// <param name="toDelete"></param>
        private static void DeleteWithoutSumitting(EntityObject toDelete)
        {
            db.DeleteObject(toDelete);
        }

        /// <summary>
        /// Deletes record from db.
        /// </summary>
        /// <param name="toDelete"></param>
        internal static void Delete(EntityObject toDelete)
        {
            try
            {
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    db.DeleteObject(toDelete);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            catch(Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                db.Connection.Close();
            }

        }

        internal static void SetVisability(Value toDelete, bool visible)
        {
            toDelete.Visible = visible;
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    db.Refresh(System.Data.Objects.RefreshMode.ClientWins, toDelete);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            catch(Exception e)
            {
                throw new ConnectionException(e);
            }
            finally
            {
                db.Connection.Close();
            }

        }

        internal static string GetValue(Project project, String systemName)
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

        internal static void AddProperties(string[] properties, Project project, bool visible, bool important, String author)
        {
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    foreach (String property in properties)
                    {
                        Value toAdd = Value.CreateValue(property, project.Id, visible, important, author);
                        db.AddToValues(toAdd);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            catch(Exception e)
            {
                throw new ConnectionException(e);

            }
            finally
            {
                db.Connection.Close();
            }
        }

        /// <summary>
        /// Sets available values for specified property instead of existing.
        /// </summary>
        /// <param name="availablevalues"></param>
        /// <param name="property"></param>
        internal static void SetAvailableValues(IEnumerable<String> availablevalues, Property property)
        {
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    lock (AvailableValue.Locker)
                    {
                        foreach (AvailableValue value in property.AvailableValues)
                        {
                            DeleteWithoutSumitting(value);
                        }
                        foreach (String value in availablevalues)
                        {
                            AvailableValue availableValue = AvailableValue.CreateAvailableValue(property.SystemName);
                            db.AddToAvailableValues(availableValue);
                        }
                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            finally 
            {
                db.Connection.Close(); 
            }
        }


        internal static Property GetProperty(string systemName)
        {
            return db.Properties.FirstOrDefault(x => x.SystemName == systemName);
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
            try
            {
                db.Connection.Open();
                Value oldVersion = db.Values.FirstOrDefault(x => x.Id == entity.Id);
                if(oldVersion == null)
                {
                    throw new IllegalDBOperationException();
                }
                if(oldVersion.Equals(entity))
                {
                    return true;
                }
                using(DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    History forOldVersion = new History(oldVersion);
                    db.AddToHistories(forOldVersion);
                    db.Refresh(RefreshMode.ClientWins, entity);
                    transaction.Commit();
                    operationSuccess = true;
                }
            }
            finally
            {
                db.Connection.Close();
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
            try
            {
                db.Connection.Open();
                using (DbTransaction transaction = db.Connection.BeginTransaction())
                {
                    db.Refresh(RefreshMode.ClientWins, entity);
                    transaction.Commit();
                }
                db.Connection.Close();
                return true;
            }
            catch(Exception e)
            {
                db.Connection.Close();
                return false;
            }
        }

    }
}
