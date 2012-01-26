using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;

namespace DAL.Helpers
{
    public static class ConnectionHelper
    {
        private static ISessionFactory sessionFactory;
        public static void LoadORM()
        {
            Configuration configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Property).Assembly);
            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
