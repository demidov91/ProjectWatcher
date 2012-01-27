using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Project
{
    public class ProjectWithValuesModel
    {
        IProject entity;

        public ProjectWithValuesModel(IProject entity)
        {
            this.entity = entity;
 
        }

        public DateTime LastChanged
        {
            get 
            {
                return entity.LastChanged;
            }
            set 
            {
                entity.LastChanged = value;
            }
        }

        public IEnumerable<ValueModel> Values
        {
            get
            {
                List<ValueModel> toReturn = new List<ValueModel>();
                foreach (IValue original in entity.GetValues())
                {
                    toReturn.Add(new ValueModel(original));
                }
                return toReturn.ToArray();
            }
        }

        public String Owner
        {
            get 
            {
                return entity.GetValue("owner").ToString();
            }
        }

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


        public String GetValue(String systemName)
        {
            IValue toReturn = entity.GetValues().FirstOrDefault(x => x.SystemName == systemName);
            return (toReturn != null ? toReturn.ToString() : "");
        }



    }
}