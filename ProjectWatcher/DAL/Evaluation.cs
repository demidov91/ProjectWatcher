using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    public class Evaluation
    {
        public Dictionary<String, String> Formulas
        {
            get;
            set;
        }

        public Dictionary<String, String> Values
        {
            get;
            set;
        }

        public Evaluation()
        {
            Formulas = new Dictionary<string, string>();
            Values = new Dictionary<string, string>();
        }
    }
}