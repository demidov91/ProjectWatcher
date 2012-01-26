using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class History
    {
        public History(Value oldVersion)
        {
            ValueId = oldVersion.Id;
            Time = oldVersion.Time;
            Author = oldVersion.Author;
        }

        public History()
            :base()
        {
        }
    }
}
