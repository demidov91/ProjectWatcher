using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;

namespace ProjectWatcher.Models.Project.Index
{
    public class HistoryModel
    {
        private ValueModel parent;  

        public HistoryModel(ValueModel parent)
        {
            this.parent = parent;
        }


        public String SystemName
        {
            get { return parent.SystemName; }
        }

        public IEnumerable<IHistory> GetEvents()
        {
            return parent.GetHistories();
        }


    }
}