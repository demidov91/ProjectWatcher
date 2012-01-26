using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace ProjectWatcher.Models.Project
{
    public class PropertyModel
    {
        private DAL.Property naturalProperty;

        public PropertyModel()
        {
            naturalProperty = new Property(); 
        }

        public PropertyModel(DAL.Property naturalProperty)
        {
            this.naturalProperty = naturalProperty;
        }

        public String Name
        {
            get
            {
                return naturalProperty.Name;
            }
            set
            {
                naturalProperty.Name = value;
            }
        }

        public String SystemName
        {
            get
            {
                return naturalProperty.SystemName;
            }
            set
            {
                naturalProperty.SystemName = value;
            }
        }

        public String Type
        {
            get
            {
                return naturalProperty.Type;
            }
            set
            {
                naturalProperty.Type = value;
            }
        }

        public String AvailableValues
        {
            get
            {
                if (naturalProperty.AvailableValues == null)
                {
                    return "";
                }
                return String.Concat(naturalProperty.AvailableValues.Select(x => x + '\n'));
            }
            set
            {
                naturalProperty.AvailableValues = value.Split('\n').Where(x => Validation.TypeValidationHelper.IsValidDisplayName(x)).ToArray();
            }
        }


        public String[] AvailableValuesAsArray
        {
            get 
            {
                return naturalProperty.AvailableValues.ToArray();
            }
            set
            {
                naturalProperty.AvailableValues = value;
            }
 
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PropertyModel input = obj as PropertyModel;
            if (input == null)
            {
                return false;
            }
            return SystemName.Equals(input.SystemName);
        }





    }
}