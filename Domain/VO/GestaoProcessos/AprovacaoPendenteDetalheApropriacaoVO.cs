using System;

namespace Domain.VO.GestaoProcessos
{

    public class AprovacaoPendenteDetalheApropriacaoVO
    {
        public String CodigoGrupoPagamento { get; set; }
        public String DescricaoGrupoPagamento { get; set; }
        public Decimal Valor { get; set; }
        public String CodigoCentroCusto { get; set; }
        public String DescricaoCentroCusto { get; set; }
        public String CodigoProjeto { get; set; }
        public String DescricaoProjeto { get; set; }
        public String CodigoFluxoCaixa { get; set; }
        public String DescricaoFluxoCaixa { get; set; }
        public String Historico { get; set; }
        public String CodigoModeloRateio { get; set; }
        public String DescricaoModeloRateio { get; set; }
        public String Sistema { get; set; }
        public String NumeroRequisicao { get; set; }
    }
}
