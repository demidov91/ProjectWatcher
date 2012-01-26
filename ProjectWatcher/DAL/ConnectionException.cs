using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ConnectionException: DALException
    {
        protected String message = String.Empty;

        public override string Message
        {
            get
            {
                return message;
            }
        }

        public new Exception InnerException
        {
            get;
            set;
        }

        public ConnectionException(Exception e)
        {
            this.message = e.Message;
            InnerException = e;
        }
    }
}
