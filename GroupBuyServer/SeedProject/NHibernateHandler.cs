﻿using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace SeedProject
{
    public static class NHibernateHandler
    {
        private static ISessionFactory _sessionFactory = null;
        private static ISession _currSession = null;

        static NHibernateHandler()
        {
            _sessionFactory = InitializeSessionFactory();
        }

        public static ISession CurrSession {
            get
            {
                if (_currSession == null)
                {
                    return OpenSession();
                }
                return _currSession;
            }
            private set { _currSession = value; }
        } 

        private static ISessionFactory InitializeSessionFactory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connStr).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }

        private static ISession OpenSession()
        {
             return _sessionFactory.OpenSession();
        }
    }
}