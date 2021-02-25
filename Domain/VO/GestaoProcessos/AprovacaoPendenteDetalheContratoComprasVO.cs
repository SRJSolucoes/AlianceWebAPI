using System;

namespace Domain.VO.GestaoProcessos
{
    public class AprovacaoPendenteDetalheContratoComprasVO : AprovacaoPendenteDetalheRequisicaoVO
    {
        public String CodigoItem { get; set; }
        public String DescricaoNaturezaItem { get; set; }
        public String DescricaoTipoItemContrato { get; set; }
        public String Unidade { get; set; }
        public String UnidadeDescricao { get; set; }
        public Decimal? ValorUnitario { get; set; }
        public String EspecificacaoTecnica { get; set; }
    }
}