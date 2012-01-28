using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public abstract class DALException: Exception
    {
        protected String message = String.Empty;

        public override string Message
        {
            get
            {
                return message;
            }
        }

        public virtual new Exception InnerException
        {
            get;
            set;
        }

        
    }
}
