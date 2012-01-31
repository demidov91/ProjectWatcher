using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Project.Index
{
    public class AvailableValueModel
    {
        private IAvailableValue entity;

        public AvailableValueModel(IAvailableValue entity, bool isChecked)
        {
            this.entity = entity;
            this.IsChecked = isChecked;
        }

        /// <summary>
        /// Format equal IValue.Value is expected.
        /// </summary>
        public Object Value
        {
            get { return entity.GetValue(); }
        }

        public int Id
        {
            get { return entity.Id; }
        }

        public Boolean IsChecked
        { 
            get;
            set;
        }


    }
}