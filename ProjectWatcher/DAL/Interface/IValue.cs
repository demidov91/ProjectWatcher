using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IValue: IEntity
    {
        String SystemName
        {
            get;
            set;
        }

        Object GetValue();

        void SetValue(Object value);

        Int32 ProjectId
        {
            get;
            set;
        }

        Int32 Id
        {
            get;
            set;
        }

        Boolean Visible
        {
            get;
            set;
        }

        Boolean Important
        {
            get;
            set;
        }

        String Author
        {
            get;
            set;
        }

        DateTime Time
        {
            get;
            set;
        }

        IProperty GetProperty();

        void SetProperty(IProperty value);

        IProject GetProject();

        IEnumerable<IHistory> GetHistories();

        IValue GetCopy();

    }
}
