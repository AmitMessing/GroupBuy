using FluentNHibernate.Cfg;
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
        private static readonly ISessionFactory _sessionFactory = null;
        private static ISession _currSession = null;

        static NHibernateHandler()
        {
            _sessionFactory = InitializeSessionFactory();
        }

        public static ISession GetSession {
            get
            {
                if (_currSession == null)
                {
                    _currSession = _sessionFactory.OpenSession(); 
                }
                return _currSession;
            }
        }

        private static ISessionFactory InitializeSessionFactory()
        {
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                                .ConnectionString(@"Server=.\SQLEXPRESS;Database=GroupBuy;User Id=Yoni-PC\Yoni;")
                                .ShowSql()
                    )
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    .BuildSessionFactory();
        }
    }
}