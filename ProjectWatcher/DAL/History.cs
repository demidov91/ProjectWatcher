using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interface;

namespace DAL
{
    partial class History: IHistory
    {
        public History(Value oldVersion)
        {
            ValueId = oldVersion.Id;
            Time = oldVersion.Time;
            Author = oldVersion.Author;
            Value = oldVersion.Value1;
        }

        public History()
            :base()
        {
        }

        public Object FormerValue
        {
            get { return Value; }

        }




    }
}
