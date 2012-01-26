using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class Property
    {
        public virtual String Name
        {
            get;
            set;
        }

        public virtual String SystemName
        {
            get;
            set;
        }

        public virtual String Type
        {
            get;
            set;
        }

        public virtual IList<String> AvailableValues
        {
            get;
            set;
        }




        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Property input = obj as Property;
            if (input == null)
            {
                return false;
            }
            return SystemName.Equals(input.SystemName);            
        }

    }
   
}