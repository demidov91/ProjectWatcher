using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Models.Projects
{
    public class TableModel
    {
        public String[] Headers
        {
            get;
            set;
        }

        public ProjectModel[] Projects
        {
            get;
            set;
        }

        public int[] Width
        {
            get;
            set;
        }

        public String TableDefinition
        {
            get;
            set;
        }

        public String Filter
        {
            get;
            set;
        }

        public String[][] AllValues
        {
            get
            {
                String[][] toReturn = new String[Projects.Length][];
                for(int i = 0; i < Projects.Length; i++)
                {
                    toReturn[i] = new String[Headers.Length];
                    for(int j =0; j < Headers.Length; j++)
                    {
                        toReturn[i][j] = Projects[i].Properties[j];
                    }
                }
                return toReturn;
            }

        }
    }
}