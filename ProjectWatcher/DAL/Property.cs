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
        public Property()
        { }

        public Property(IProperty parent)
        {
            this.SystemName = parent.SystemName;
            this.DisplayName = parent.DisplayName;
            this.Type = parent.Type;
        }



        public IEnumerable<IAvailableValue> GetAvailableValues()
        {
            return this.AvailableValues;
        }


        
        
        

    }
}
