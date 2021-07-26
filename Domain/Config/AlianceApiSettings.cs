using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class AlianceApiSettings
    {
        public string TokenDefault { get; set; }
        public DatabaseConfig DatabaseConfig { get; set; }
        public DatabaseConfigFromSO DatabaseConfigFromSO { get; set; }
        public SODatabaseVariables SODatabaseVariables { get; set; }
        public ControleApiSettings ControleApiSettings { get; set; }
        public WSGestaoProcessosettings WSGestaoProcessosettings { get; set; }

        public static void ConfigurarSoDatabaseVariables(AlianceApiSettings settings, IConfiguration Configuration)
        {
            settings.DatabaseConfigFromSO.Usuario = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Usuario").Value).Value;
            settings.DatabaseConfigFromSO.Senha = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Senha").Value).Value;
            settings.DatabaseConfigFromSO.Host = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Host").Value).Value;
            settings.DatabaseConfigFromSO.ServiceName = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:ServiceName").Value).Value;
            settings.DatabaseConfigFromSO.SID = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:SID").Value).Value;
            settings.DatabaseConfigFromSO.AmbWs = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:AmbWs").Value).Value;
            settings.DatabaseConfigFromSO.Port = Configuration.GetSection(Configuration.GetSection("AlianceApiSettings:SODatabaseVariables:Port").Value).Value;
        }

    }

    public class DatabaseConfig
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Host { get; set; }
        public string ServiceName { get; set; }
        public string SID { get; set; }
        public string AmbWs { get; set; }
        public string Port { get; set; }
    }

    public class DatabaseConfigFromSO : DatabaseConfig
    {
    }
    public class SODatabaseVariables
    {
        public bool ActiveDBfromSO { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Host { get; set; }
        public string ServiceName { get; set; }
        public string SID { get; set; }
        public string AmbWs { get; set; }
        public string Port { get; set; }
    }


    public class ControleApiSettings
    {
        public bool Active { get; set; }
        public string HostApi { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string TokenSecret { get; set; }
        // Access Token
        public string TokenValue { get; set; }
        public string UserID { get; set; }
        public bool ForcedDB { get; set; }
        public string ForcedDBHost { get; set; }
        public string ForcedDBServiceName { get; set; }
        public string ForcedDBPort { get; set; }
    }

    public class WSGestaoProcessosettings
    {
        public string Url { get; set; }
    }

}
