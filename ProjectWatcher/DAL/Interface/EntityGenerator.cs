using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public static class EntityGenerator<T>
        where T: IEntity
    {
        public static IEntity CreateEntity()
            
        {
            if (typeof(T).Equals(typeof(IValue)))
                return new Value();
            if (typeof(T).Equals(typeof(IProperty)))
                return new Property();
            return null;
        }

    }
}
