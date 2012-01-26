using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using SystemSettings;

namespace ProjectWatcher.Models.Project
{
    public class PropertyModel
    {
        private Property naturalProperty;

        private String localAvailableValues = null;

        public PropertyModel()
        {
            naturalProperty = new Property(); 
        }

        public PropertyModel(DAL.Property naturalProperty)
        {
            if (naturalProperty != null)
            {
                this.naturalProperty = naturalProperty;
            }
            else
            {
                naturalProperty = new Property();
            }
        }

        public String Name
        {
            get
            {
                return naturalProperty.DisplayName;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                naturalProperty.DisplayName = value;
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
                if (value != null)
                {
                    naturalProperty.SystemName = value;
                }
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
                if (value != null)
                {
                    naturalProperty.Type = value;
                }
            }
        }

        public String AvailableValues
        {
            get
            {
                if (localAvailableValues == null)
                {
                    return String.Concat(naturalProperty.AvailableValues.Select(x => x.Value + '\n'));
                }
                return localAvailableValues;
            }
            set
            {
                if (value != null)
                {
                    localAvailableValues = value;
                }
            }
        }


        public String[] AvailableValuesAsArray
        {
            get 
            {
                if (localAvailableValues == null)
                {
                    return naturalProperty.AvailableValues.Select(x => x.Value).ToArray();
                }
                return localAvailableValues.Split('\r', '\n');
            }
            set
            {
                localAvailableValues = String.Concat(value.Select(x => x + '\n'));
            }
 
        }

        /// <summary>
        /// This attribute of value but it is necessary for "CreationOfNewProperty" view
        /// </summary>
        public bool IsImportant
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
            PropertyModel input = obj as PropertyModel;
            if (input == null)
            {
                return false;
            }
            return SystemName.Equals(input.SystemName);
        }





    }
}