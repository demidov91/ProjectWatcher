using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Objects.DataClasses;
using DAL.Helpers;


namespace DAL.Interface
{
    /// <summary>
    /// Class for modifying projects' properties values
    /// </summary>
    public class Modifier
    {
        /// <summary>
        /// To implement. Should load properrties from csv-file
        /// </summary>
        /// <param name="input">Stream for csv-file</param>
        /// <returns>if download was successful</returns>
        public bool Load(Stream input)
        {
            return true;                         
        }


        public bool Modify(IEntity entity)
        {
            if(entity is IValue)
            {
                return ConnectionHelper.ModifyWithHistory(new Value((IValue)entity));
            }
            else
            {
                return ConnectionHelper.Modify(entity);
            }
        }




    }
}
