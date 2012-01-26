using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helpers;
using SystemSettings;

namespace DAL
{
    public partial class Project
    {

        /// <summary>
        /// Gets all neccessary parameters for creation Project entity and creates it out of DB.
        /// </summary>
        /// <param name="created">Time of creation of the project.</param>
        /// <param name="lastChanged"></param>
        /// <returns></returns>
        public static Project CreateProject(DateTime created, DateTime lastChanged)
        {
            return new Project { Created = created, LastChanged = lastChanged };
        }

        /// <summary>
        /// Hides property.
        /// </summary>
        /// <param name="systemName">Property to hide.</param>
        public void DeleteProperty(String systemName)
        {
            Value toDelete = Values.FirstOrDefault(x => x.SystemName == systemName);
            if (toDelete != null)
            {
                ConnectionHelper.SetVisability(toDelete, false);
            }
        }

        /// <summary>
        /// Shows or adds property to project.
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="visible">If new property should be visible (it has now difference for existing properties).</param>
        /// <param name="important"></param>
        /// <param name="author">User who made this changing.</param>
        /// <exception cref="ConnectionException"></exception>
        public void AddProperty(String systemName, bool visible, bool important, String author)
        {
            if (SystemSettings.TypeValidationHelper.IsValidSystemName(systemName))
            {
                Value toAdd = this.Values.FirstOrDefault(x => x.SystemName == systemName);
                if (toAdd != null)
                {
                    ConnectionHelper.SetVisability(toAdd, true);
                }
                else
                {
                    ConnectionHelper.AddProperties(new String[] { systemName }, this, visible, important, author);
                }
            }
        }


        public String Name
        {
            get
            {
                try
                {
                    return ConnectionHelper.GetValue(this, "name");
                }
                catch (BadSystemNameException e)
                {
                    return "";
                }
            }

        }

        public String Owner
        {
            get 
            {
                try
                {
                    return ConnectionHelper.GetValue(this, "owner");
                }
                catch (BadSystemNameException e)
                {
                    return "";
                }
            }
        }

        

        /// <summary>
        /// Returns value of specified property for current project.
        /// </summary>
        /// <param name="systemName">Name of the property or some string that wouldn't be avaluated.</param>
        /// <returns>Value of property or input string.</returns>
        public String GetValue(String systemName)
        {
            return ConnectionHelper.GetValue(this, systemName);
        }
    }
}
