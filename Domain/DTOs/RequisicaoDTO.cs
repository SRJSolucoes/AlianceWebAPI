    using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class RequisicaoDTO
    {
        public String ReqNumero { get; set; }
        public Int64 SqAprovacao { get; set; }
        public Int64 SqItemAprovacao { get; set; }
        public Int64 SqItemLinhaAprov { get; set; }
        public String Usuario_Aprovador { get; set; }
        public DateTime ReqData { get; set; }
        public String Empresa { get; set; }
        public String Empresa_Nome { get; set; }
        public String Moeda { get; set; }
        public String DS_Moeda { get; set; }
        public String TipoReq { get; set; }
        public String DS_TipoReq { get; set; }
        public String Setor { get; set; }
        public String Requisitante { get; set; }
        //public String Cat_Funcional { get; set; }
        //public String Posicao_Funcional { get; set; }
        //public String DSPosicao_Funcional { get; set; }
        public String Observacao { get; set; }
        public DateTime DataMov { get; set; }
        public String Etape { get; set; }
        public String Tpoper { get; set; }
        public String Reqcompra { get; set; }
        public String Usuario { get; set; }
        public Int64 Ordem { get; set; }
        public String Justificativa { get; set; }
        public String Devolucao { get; set; }
        public String Protocolo { get; set; }
        public String ForaPrazo { get; set; }
        public String StatusAprv { get; set; }
        public String Impresso { get; set; }
        public String Encerram { get; set; }
        public String Destinacao { get; set; }
        public String CCusto { get; set; }
        public String Fornecedor { get; set; }
        public String Fornecedor_Nome { get; set; }
        public String Tipo { get; set; }
        public Double Valor_Total { get; set; }

    }
}
