using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    class AvailableValue
    {
        public virtual String Value
        {
            get;
            set;
        }
        public virtual String Property
        {
            get;
            set;
        }

        private int Id
        {
            get;
            set;

        }
    }
}
