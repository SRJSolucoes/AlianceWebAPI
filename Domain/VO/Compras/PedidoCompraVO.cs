using System;

namespace Domain.VO.Compras
{
    public class PedidoCompraVO
    {
        public String CodigoEmpresa { get; set; }
        public String NumeroPedido { get; set; }
        public String NumeroRequisicao { get; set; }
        public String CodigoItem { get; set; }
        public String DescricaoItem { get; set; }
        public String Unidade { get; set; }
        public String Mapa { get; set; }
        public Decimal? Quantidade { get; set; }
        public Decimal? Valor { get; set; }
        public Decimal? ValorTotalItem { get; set; }
        public Int64? SequenciaAprovacao { get; set; }
        public long Sequencia { get; set; }
        public String NomeFornecedor { get; set; }
        public String TipoRequisicao { get; set; }
        public String Observacao { get; set; }

        /// <summary>
        /// Atributos para Projeto específico 
        /// Não foi criado outro VO devido a complexidade do processo Aprovações Pendentes
        /// Caso fosse criado outro VO para ser usado somente em Projeto específico
        /// teria que alterar toda a rotina de Aprovações Pendentes do Manager para classe genérica
        /// </summary>
        public String Justificativa { get; set; }
        public String CentroCusto { get; set; }
        public String CodigoRequisitante { get; set; }
        public String DescricaoRequisitante { get; set; }
    }
}
