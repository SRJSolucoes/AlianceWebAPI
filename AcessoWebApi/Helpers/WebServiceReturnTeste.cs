using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PadraoWebApi.Helpers
{
    [Serializable()]
    public class WebServiceReturnTeste
    {
        public Atributo[] Atributos { get; set; }
        public ErroMSG[] ErroMSGs { get; set; }
        public Registro[] Registros { get; set; }
    }

    [Serializable()]
    public class ErroMSG
    {
        public string ermCDErro { get; set; }
        public string ermDSErro { get; set; }
        public string ermMensagemAux { get; set; }
        public string ermErro { get; set; }
    }
    
    [Serializable()]
    public class Atributo
    {
        public string testes { get; set; }
    }

    [Serializable()]
    public class Registro
    {
        public int wsrrSequencial { get; set; }
        public string wsrrTipo { get; set; }
        public Campo[] Campos{ get; set; }
    }
    
    [Serializable()]
    public class Campo
    {
        public string wsrcNome { get; set; }
        public string wsrcValor { get; set; }
    }
}
