using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IHistory: IEntity
    {
        String Author{ get; }


        DateTime Time { get; }

        Object FormerValue { get; }
    }
}
