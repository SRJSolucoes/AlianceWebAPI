using Domain.Enum;
using Domain.Enum.Core;
using Domain.Models;
using Domain.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades
{
    public class Requisicao : EntidadeBase
    {
        public virtual String ReqNumero { get; set; }
        public virtual DateTime ReqData { get; set; }
        public virtual String Empresa { get; set; }
        public virtual String Empresa_Nome { get; set; }
        public virtual String Moeda { get; set; }
        public virtual String DS_Moeda { get; set; }
        public virtual String TipoReq { get; set; }
        public virtual String DS_TipoReq { get; set; }
        public virtual String Setor { get; set; }
        public virtual String Requisitante { get; set; }
        public virtual String Posicao_Funcional { get; set; }
        public virtual String DSPosicao_Funcional { get; set; }
        public virtual String Observacao { get; set; }
        public virtual DateTime DataMov { get; set; }
        public virtual String Etape { get; set; }
        public virtual String Tpoper { get; set; }
        public virtual String Reqcompra { get; set; }
        public virtual String Usuario { get; set; }
        public virtual Int64 Ordem { get; set; }
        public virtual String Justificativa { get; set; }
        public virtual String Devolucao { get; set; }
        public virtual String Protocolo { get; set; }
        public virtual String ForaPrazo { get; set; }
        public virtual String StatusAprv { get; set; }
        public virtual String Impresso { get; set; }
        public virtual String Encerram { get; set; }
        public virtual String Destinacao { get; set; }
        public virtual String CCusto { get; set; }
        public virtual String Fornecedor { get; set; }
        public virtual String Fornecedor_Nome { get; set; }
        public virtual String Tipo { get; set; }
        public virtual Double Valor_Total { get; set; }
        public virtual Int64 SqAprovacao { get; set; }

    }
}
