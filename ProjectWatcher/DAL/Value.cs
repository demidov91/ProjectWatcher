using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class Value
    {
        /// <summary>
        /// Gets all nessesary parameters and creates entity of "Value" class without id.
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="projectId"></param>
        /// <param name="visible"></param>
        /// <returns>Entity not saved in DB.</returns>
        public static Value CreateValue(String systemName, int projectId, bool visible, bool important, String author)
        {
            return new Value { SystemName = systemName, ProjectId = projectId, Visible = visible,
                Value1 = Helpers.PropertiesHelper.DefaultValue(systemName), Important = important,
                Author = author, Time = DateTime.Now
            };
        }

        public override int GetHashCode()
        {
            return (Id + Time.Date.DayOfYear)%Int32.MaxValue;
        }

        public override bool Equals(object obj)
        {
            Value other = obj as Value;
            if( other == null)
            {
                return false;
            }
            return Id == other.Id && Time == other.Time && Value1 == other.Value1 && Author == other.Author;
        }
    }
}
