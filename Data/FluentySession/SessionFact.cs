using Data.Mapping;
using NHibernate;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Net;

namespace Data.FluentySession
{
    public static class SessionFact
    {
        private static IFluentySessionFactory frameworkSessionFactoryUserPas;
        private static IFluentySessionFactory frameworkSessionFactory;
        private static IFluentySessionFactory frameworkSessionFactoryInput;
        private static IFluentySessionFactory frameworkSessionFactoryOutPut;

        public static ISessionFactory GetSessionFact(string usuario, string senha, string serviceName = null, string host = null, string port = null)
        {
            string connectionStringOracle = getOracleConnectionString(usuario, senha, serviceName, host, port);
            frameworkSessionFactoryUserPas = new FluentySessionFactory<AnexoRequisicaoMap>(connectionStringOracle, "oracle");

            return frameworkSessionFactoryUserPas.CreateSessionFactory();
        }

        private static string getOracleConnectionString(string usuario, string senha, string serviceName, string host, string port)
        {
            var hostName = String.IsNullOrWhiteSpace(host) ? "10.0.100.23" : host;
            var hostPort = String.IsNullOrWhiteSpace(port) ? "1521" : port;
            var hostServiceName = String.IsNullOrWhiteSpace(serviceName) ? "HOM" : serviceName;

            var connectionStringOracle = $@"Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {hostName})(PORT = {hostPort})))(CONNECT_DATA =(SERVICE_NAME = {hostServiceName})));User Id={usuario};Password={senha};";
            return connectionStringOracle;
        }

        public static ISessionFactory GetSessionFact()
        {
            if (frameworkSessionFactory == null)
            {
                // TODO Ajustar pro oracle
                //var connectionStringMySQL = "Server = acesso.cyzeslms577w.us-east-2.rds.amazonaws.com; Port = 3306; Database = Acesso; Uid = O2Si; Pwd = O2SiT3cnologia";
                //frameworkSessionFactory = new FluentySessionFactory<RequisicaoMap>(connectionStringMySQL, "mysql");

                //var connectionStringMySQL = "SERVER = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.100.23)(PORT = 1521))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = HOM))); uid = ATUAL_SHP; pwd = ATUAL_SHP";

                // connectionstring template copiado do ItabusCore
                var connectionStringMySQL = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.100.23)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = HOM)));User Id=ATUAL_SHP;Password=ATUAL_SHP;";
                frameworkSessionFactory = new FluentySessionFactory<AnexoRequisicaoMap>(connectionStringMySQL, "oracle");

            }
            return frameworkSessionFactory.CreateSessionFactory();
        }

        public static ISessionFactory GetSessionFactInput()
        {
            if (frameworkSessionFactoryInput == null)
            {
                var connectionStringMySQL = "Server =instituicaodeeducacao.cyzeslms577w.us-east-2.rds.amazonaws.com; Port=3306; Database=CadastrosMarketPlace; Uid=O2Siadmin; Pwd =123O2Siadmin123";
                frameworkSessionFactoryInput = new FluentySessionFactory<O2sicontroleMap>(connectionStringMySQL, "mysql");
            }
            return frameworkSessionFactoryInput.CreateSessionFactory();
        }

        public static ISessionFactory GetSessionFactOutput()
        {
            if (frameworkSessionFactoryOutPut == null)
            {
                var connectionStringMySQL = "Server =instituicaodeeducacao.cyzeslms577w.us-east-2.rds.amazonaws.com; Port=3306; Database=CadastrosMarketPlace; Uid=O2Siadmin; Pwd =123O2Siadmin123";
                frameworkSessionFactoryOutPut = new FluentySessionFactory<O2sicontroleMap>(connectionStringMySQL, "mysql");
            }
            return frameworkSessionFactoryOutPut.CreateSessionFactory();
        }
    }
}
