using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Objects.DataClasses;
using DAL.Helpers;


namespace DAL
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


        public bool Modify(EntityObject entity)
        {
            if(entity is Value)
            {
                return ConnectionHelper.ModifyWithHistory((Value)entity);
            }
            else
            {
                return ConnectionHelper.Modify(entity);
            }
        }




    }
}
