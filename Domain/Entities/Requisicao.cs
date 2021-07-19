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
        //public virtual String Posicao_Funcional { get; set; }
        //public virtual String DSPosicao_Funcional { get; set; }
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
        public virtual Int64 SqItemLinhaAprov { get; set; }
        //public virtual String Cat_Funcional { get; set; }
        public virtual Int64 SqItemAprovacao { get; set; }
        public virtual String Usuario_Aprovador { get; set; }

        public virtual String Cod_Requisitante { get; set; }
        public virtual String Nome_Requisitante { get; set; }
        public virtual String Documento { get; set; }
        public virtual String Cod_Tipocobranca { get; set; }
        public virtual String Tipo_Cobranca { get; set; }
        public virtual String Posicao_Funcional { get; set; }
        public virtual String DSPosicao_Funcional { get; set; }
        public virtual String Desc_Pagamento { get; set; }
        public virtual String Cod_Emp_Origem { get; set; }
        public virtual String Nome_Emp_Origem { get; set; }
        public virtual String Dt_Prog_Pag { get; set; }
        public virtual String Observacao_Titulo { get; set; }


    }
}
