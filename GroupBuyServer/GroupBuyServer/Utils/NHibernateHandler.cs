﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using System.Reflection;

namespace GroupBuyServer.Utils
{
    public static class NHibernateHandler
    {
        private static ISession _currSession;
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
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(@"Server=.\SQLEXPRESS;Database=GroupBuy;User Id=Amit; Password=12345;")
                    .ShowSql()
                ).Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    .BuildSessionFactory();
        }

        private static ISession OpenSession()
        {
             return InitializeSessionFactory().OpenSession();
        }
    }
}