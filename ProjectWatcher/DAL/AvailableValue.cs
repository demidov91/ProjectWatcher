using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class AvailableValue
    {
        private static Object locker = new Object();

        public static Object Locker
        {
            get
            {
                return locker;
            }
        }


        public static AvailableValue CreateAvailableValue(String systemName)
        {
            return new AvailableValue { Property = systemName };
 
        }
        
    }
}
