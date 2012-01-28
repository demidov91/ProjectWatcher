using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Project.Index
{
    public class ProjectWithValuesModel
    {
        private IProject entity;

        public ProjectWithValuesModel(IProject entity)
        {
            this.entity = entity;
 
        }

        public Int32 ProjectId
        {
            get 
            {
                return entity.Id;
            }
        }

        public String LastUser
        {
            get 
            {
                return entity.LastImportantChanging == null 
                    ? ""
                    : entity.LastImportantChanging.Author;
            }
        }

        public DateTime? LastChanged
        {
            get 
            {
                if(entity.LastImportantChanging == null)
                {
                    return null;
                }
                return entity.LastImportantChanging.Time; 
            }
        }

        /// <summary>
        /// If this user can modify properties.
        /// </summary>
        public Boolean IsEditable
        {
            get;
            set;
        }

        /// <summary>
        /// Models for each value. Readonly.
        /// </summary>
        public IEnumerable<ValueModel> Values
        {
            get
            {
                List<ValueModel> toReturn = new List<ValueModel>();
                foreach (IValue original in entity.GetValues())
                {
                    toReturn.Add(new ValueModel(original, IsEditable));
                }
                return toReturn.ToArray();
            }
        }

        /// <summary>
        /// Owner of this project. Readonly.
        /// </summary>
        public String Owner
        {
            get 
            {
                return entity.GetValue("owner").ToString();
            }
        }

        /// <summary>
        /// Name, caption of the project.
        /// </summary>
        public String Name
        {
            get 
            {
                return entity.GetValue("name");
            }
            set 
            {
                try{
                    IValue name = entity.GetValues().FirstOrDefault(x => x.SystemName == "name");
                    name.SetValue(value);
                }
                catch(BadSystemNameException e)
                {

                }
            }
        }

        /// <summary>
        /// Shows text value for specified property.
        /// </summary>
        /// <param name="systemName">Systemname of the property.</param>
        /// <returns></returns>
        public String GetValue(String systemName)
        {
            IValue toReturn = entity.GetValues().FirstOrDefault(x => x.SystemName == systemName);
            return (toReturn != null ? toReturn.ToString() : "");
        }



    }
}