using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public class IllegalDBOperationException: DALException
    {
        private string property;

        public Object BadValue
        {
            get;
            private set;
        }
            
        public IllegalDBOperationException(IEntity badValue)
        {
            this.BadValue = badValue;
        }

<<<<<<< HEAD
        public IllegalDBOperationException(InvalidOperationException e)
        {
            InnerException = e;
        }

=======
        
>>>>>>> master

    }
}
