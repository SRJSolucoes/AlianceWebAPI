using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using Configuration = NHibernate.Cfg.Configuration;

namespace Data.FluentySession
{
    public class FluentySessionFactory<T> : IFluentySessionFactory where T : class
    {
        private const string DATABASE_MYSQL = "mysql";

        private readonly string _connectionString;
        private readonly string _databaseEngine;

        private ISessionFactory _sessionFactory;

        public FluentySessionFactory(
            string connectionString = null,
            string databaseEngine = "oracle"
        )
        {
            if (!string.IsNullOrEmpty(connectionString))
                _connectionString = connectionString;

            if (!string.IsNullOrEmpty(databaseEngine))
                _databaseEngine = databaseEngine;

            if (string.IsNullOrEmpty(_connectionString)) throw new ArgumentNullException();
        }

        public ISessionFactory CreateSessionFactory()
        {
            return _sessionFactory ?? (_sessionFactory = CreateFactory());
        }

        public void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, false);
            config.Properties.Add("current_session_context_class", "web");
        }

        private ISessionFactory CreateFactory()
        {
            // TODO Aqui que ta o erro
            IPersistenceConfigurer dbConfig;
            var bancoNoAppConfig = _databaseEngine;

            if (bancoNoAppConfig.Equals(DATABASE_MYSQL, StringComparison.InvariantCultureIgnoreCase))
            {
                dbConfig = MySQLConfiguration.Standard.ConnectionString(_connectionString);
            }
            else
            {
                dbConfig = OracleManagedDataClientConfiguration.Oracle10.ConnectionString(c => c.Is(_connectionString));
            }

            return Fluently.Configure()
                .Database(dbConfig)
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(T).Assembly))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }
    }
}
