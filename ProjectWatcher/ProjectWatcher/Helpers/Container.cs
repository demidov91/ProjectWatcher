using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Helpers
{
    public class Container<T>
    {
        public T Value
        {
            get;
            set;
        }

        public Container(T value)
        {
            Value = value;
        }

        public Container()
        { }
    
    }
}