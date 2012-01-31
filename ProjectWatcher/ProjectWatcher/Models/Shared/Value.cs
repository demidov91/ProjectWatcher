using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Shared
{
    public class Value: IValue
    {
        private IProperty property;
        private IProject project;
        private IEnumerable<IHistory> histories;
        private object value;

        public String SystemName
        {
            get;
            set;
        }

        public Object GetValue()
        {
            return value;
        }

        public void SetValue(Object value)
        {
            value = value;
        }

        public Int32 ProjectId
        {
            get;
            set;
        }

        public Int32 Id
        {
            get;
            set;
        }

        public Boolean Visible
        {
            get;
            set;
        }

        public Boolean Important
        {
            get;
            set;
        }

        public String Author
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public IProperty GetProperty()
        {
            return property;
        }

        public void SetProperty(IProperty value)
        {
            property = value;
        }

        public IProject GetProject()
        {
            return project;
        }

        public IEnumerable<IHistory> GetHistories()
        {
            return histories;
        }

        public IValue GetCopy()
        {
            return (Value)this.MemberwiseClone();
        }

    }
}