using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Helpers;

namespace DAL.Interface
{
    public static class Starter
    {
        public static bool Start()
        {
            return ConnectionHelper.LoadORM();
        }
    }
}
