﻿using NHibernate;
using NHibernate.Cfg;

namespace Infrastructure
{
    /// <summary>
    /// NHibernate帮助方法
    /// </summary>
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
//                    configuration.AddAssembly(typeof(Users).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
