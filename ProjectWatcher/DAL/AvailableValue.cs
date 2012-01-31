using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interface;

namespace DAL
{
    partial class AvailableValue: IAvailableValue
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

        public IValue GetValue()
        {
            return new Value {Value1 = Value};
        }


        
    }
}
