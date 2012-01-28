using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IProject: IEntity
    {
        IHistory LastImportantChanging
        {
            get;
        }

        /// <summary>
        /// Uniq id in database.
        /// </summary>
        Int32 Id
        {
            get;
        }

        /// <summary>
        /// Collection of actual values for properties of project.
        /// </summary>
        IEnumerable<IValue> GetValues();

        /// <summary>
        /// Hides property.
        /// </summary>
        /// <param name="systemName">Property to hide.</param>
        /// <exception cref="ConnectionException" />
        void DeleteProperty(String sytemName);

        /// <summary>
        /// Shows or adds property to project.
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="visible">If new property should be visible (it has now difference for existing properties).</param>
        /// <param name="important"></param>
        /// <param name="author">User who made this changing.</param>
        /// <exception cref="ConnectionException"></exception>
        void AddProperty(String systemName, bool visible, bool important, String author);
        
        /// <summary>
        /// Returns value of specified property for current project.
        /// </summary>
        /// <param name="systemName">Name of the property or some string that wouldn't be avaluated.</param>
        /// <returns>Value of property or input string.</returns>
        /// <exception cref="BadSystemnameException" />
        String GetValue(String systemName);
    }
}
