using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Reflection;

namespace GroupBuyServer.Utils
{
    public static class NHibernateHandler
    {
        private static ISession _currSession;
        public static ISession CurrSession
        {
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
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(@"Server=.\SQLEXPRESS;Database=GroupBuy;User Id=groupbuy; Password=groupbuy123;")
                    .ShowSql()
                ).Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildConfiguration();
//            
//            var exporter = new SchemaExport(cfg);
//            exporter.Create(false, true);

            return cfg.BuildSessionFactory();
        }

        private static ISession OpenSession()
        {
            return InitializeSessionFactory().OpenSession();
        }
    }
}