using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheRequisicaoComprasVO : AprovacaoPendenteDetalheRequisicaoVO
    {
        public String CodigoItem { get; set; }
        public String TipoRequisicao { get; set; }
        public String Unidade { get; set; }
        public Decimal? QuantidadePedida { get; set; }
        public DateTime? DataEntrega { get; set; }
        public Decimal? ValorUnitario { get; set; }
        public Decimal? ValorTotalItem { get; set; }

        /// <summary>
        /// Atributos para Projeto específico
        /// Não foi criado outro VO devido a complexidade do processo Aprovações Pendentes
        /// Caso fosse criado outro VO para ser usado somente em Projeto específico
        /// teria que alterar toda a rotina de Aprovações Pendentes do Manager para classe genérica
        /// </summary>
        public String JustificativaRequisicao { get; set; }

        public String CentroCusto { get; set; }
    }
}