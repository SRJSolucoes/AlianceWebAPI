using Data.Handlers;
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

        public static string getOracleConnectionString(string usuario, string senha)
        {
            return "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.100.23)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = HOM)));User Id=" + usuario + ";Password=" + senha + ";";
        }
        public static ISessionFactory GetSessionFact(string usuario, string senha)
        {

            var connectionStringOracle = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.100.23)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = HOM)));User Id=" + usuario + ";Password=" + senha + ";";
            frameworkSessionFactoryUserPas = new FluentySessionFactory<RequisicaoMap>(connectionStringOracle, "oracle");

            return frameworkSessionFactoryUserPas.CreateSessionFactory();
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
                frameworkSessionFactory = new FluentySessionFactory<RequisicaoMap>(connectionStringMySQL, "oracle");

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
