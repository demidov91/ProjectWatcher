using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helpers;
using DAL.Interface;

namespace DAL
{
    partial class Property: IProperty
    {
        public IEnumerable<IAvailableValue> GetAvailableValues()
        {
            return this.AvailableValues;
        }

        
        
        

    }
}
