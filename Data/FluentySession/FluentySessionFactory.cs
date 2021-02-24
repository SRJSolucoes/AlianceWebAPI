using Data.Handlers;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Net;
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

            // dbConfig = SQLiteConfiguration.Standard.ConnectionString("database.db");
            ISessionFactory sessionFactory = null;
            try
            {
                 sessionFactory = Fluently.Configure()
                    .Database(dbConfig)
                    .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(T).Assembly))
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            catch (FluentConfigurationException ex)
            {
                if (((Oracle.ManagedDataAccess.Client.OracleException)ex.InnerException).Message.Contains("ORA-01017"))
                {
                    throw new HttpStatusException(HttpStatusCode.BadRequest, "Usuario ou senha inválido");
                }
            }

            return sessionFactory;
        }
    }
}
