using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Interface;
using ProjectWatcher.Models.Shared;

namespace ProjectWatcher.Models.Project.Index
{
    public class ValueModel
    {
        protected IValue entity;

        /// <summary>
        /// Very unsafe. Deprecated.
        /// </summary>
        private ValueModel()
        {
            entity = new Value();
        }



        public ValueModel(IValue entity, bool isEditable)
        {
            this.entity = entity;
            IsEditable = isEditable;
        }

        /// <summary>
        /// Specifies valueModel to multyselect model.
        /// </summary>
        /// <returns>Valid multyselect model or null if it was impossible</returns>
        public object GetMultyselectModel()
        {
            if(Type != "Multyselect")
            {
                return null;
            }
            return new MultyselectModel(entity, IsVisible);
        }

        public IValue DalValue
        {
            get { return entity.GetCopy(); }
        }


        public String SystemName
        {
            get 
            {
                return entity.SystemName;
            }
            set 
            {
                entity.SystemName = value;
            }
        }

        public String Name
        {
            get 
            {
                return (entity == null
                    ? ""
                    : entity.GetProperty().DisplayName
                    );
            }
            set
            {
                entity.GetProperty().DisplayName = value;
            }
        }

        public Object Value
        {
            get
            {
                return (entity == null || entity.GetValue() == null
                    ? String.Empty
                    : entity.GetValue().ToString()
                    );
            }
            set
            {
                if (entity != null)
                {
                    entity.SetValue(value);
                }
            }
        }

        public Int32 Id
        {
            get
            {
                return entity.Id;
            }
            set
            {
                entity.Id = value;
            }
        }

        public Boolean IsVisible
        {
            get
            {
                return entity.Visible;
            }
            set
            {
                entity.Visible = value;
            }
        }

        public Boolean IsImportant
        {
            get
            {
                return entity.Important;
            }
            set
            {
                entity.Important = value;
            }
        }

        public String Author
        {
            get
            {
                return entity.Author;
            }
            set
            {
                entity.Author = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return entity.Time;
            }
            set
            {
                entity.Time = value;
            }
        }

        public String Type
        {
            get 
            {
                return entity.GetProperty().Type;
            }
        }

        public HistoryModel History
        {
            get
            {
                return new HistoryModel(this);
            }
        }


        /// <summary>
        /// If I should render views for editing for this property. Readonly.
        /// </summary>
        public bool IsEditable
        {
            get;
            private set;

        }

        internal IEnumerable<IHistory> GetHistories()
        {
            return entity.GetHistories();
        }

        public IProject Project
        {
            get
            {
                return entity.GetProject();
            }
        }

        
    }
}