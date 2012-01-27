using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IProperty: IEntity
    {
        String SystemName
        {
            get;
            set;
        }

        String DisplayName
        {
            get;
            set;
        }


        String Type
        {
            get;
            set;
        }

        IEnumerable<IAvailableValue> GetAvailableValues();
        
    }
}
