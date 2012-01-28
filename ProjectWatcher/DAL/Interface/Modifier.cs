using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Objects.DataClasses;
using DAL.Helpers;


namespace DAL.Interface
{
    /// <summary>
    /// Class for modifying projects' properties values
    /// </summary>
    public class Modifier
    {

        public bool Modify(IEntity entity)
        {
            if(entity is IValue)
            {
                return ConnectionHelper.ModifyWithHistory(new Value((IValue)entity));
            }
            else
            {
                return ConnectionHelper.Modify(entity);
            }
        }

        /// <summary>
        /// Modifies existing or creates new records in db. 
        /// </summary>
        /// <param name="projectId">Id of project to modify.</param>
        /// <param name="values">New values for project.</param>
        /// <param name="author">Author of changing.</param>
        /// <returns>If it were problems while processing.</returns>
        public bool ModifyOrCreate(int projectId, Dictionary<string, string> values, IUser author)
        {
            Project project;
            try
            {
                project = ConnectionHelper.GetProject(projectId);
            }
            catch (ConnectionException e)
            {
                return false;
            }
            if(project == null)
            {
                return false;
            }
            bool result = true;
            foreach (KeyValuePair<String, String> systemNameValuePair in values)
            {
                Value toModify = project.Values.FirstOrDefault(x => x.SystemName == systemNameValuePair.Key);
                if (toModify == null)
                {
                    try
                    {
                        project.AddProperty(systemNameValuePair.Key, false, true, author.Name);
                    }
                    catch (DALException e)
                    {
                        result = false;
                    }
                }
                else 
                {
                    
                    toModify.Value1 = systemNameValuePair.Value;
                    toModify.Author = author.Name;
                    toModify.Time = DateTime.Now;
                    result &= ConnectionHelper.ModifyWithHistory(toModify);
                }
            }
            return result;
        }

        public void ModifyImportance(IValue toChange)
        {
            ConnectionHelper.SetImportance(new Value(toChange));
        }
    }
}
