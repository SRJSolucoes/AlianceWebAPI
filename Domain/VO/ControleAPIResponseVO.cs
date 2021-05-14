using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.VO
{
    public class ControleAPIResponseVO
    {
        public ResponseVO Response { get; set; }
        public SenhaVO Senha { get; set; }
    }

    public class ResponseVO
    {
        public int Status { get; set; }
        public string Mensagem { get; set; }
        public bool Erro { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }

    public class SenhaVO
    {
        public string Id { get; set; }
        public object Tag { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Conteudo { get; set; }
        public string Hostname { get; set; }
        public object Senha_pai_cod { get; set; }
        public object Senha_pai { get; set; }
        public object Adicional { get; set; }
        public string Dominio { get; set; }
        public string Ip { get; set; }
        public string Porta { get; set; }
        public string Modelo { get; set; }
        public DateTime Datahora_expiracao { get; set; }
    }

}
