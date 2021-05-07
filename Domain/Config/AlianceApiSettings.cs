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
        public ControleApiSettings ControleApiSettings { get; set; }
        public WSGestaoProcessosettings WSGestaoProcessosettings { get; set; }

    }

    public class DatabaseConfig
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Host { get; set; }
        public string ServiceName { get; set; }
        public string Port { get; set; }
    }

    public class ControleApiSettings
    {
        public bool Active { get; set; }
        public string HostApi { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string TokenSecret { get; set; }
        public string TokenValue { get; set; }
        public string UserID { get; set; }
    }

    public class WSGestaoProcessosettings
    {
        public string Url { get; set; }
    }

}
